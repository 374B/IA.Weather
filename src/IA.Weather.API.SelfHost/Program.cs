using System;
using System.Linq;
using Destructurama;
using IA.Weather.API.Bindings;
using Microsoft.Owin.Hosting;
using Serilog;
using SimpleInjector;
using Topshelf;

namespace IA.Weather.API.SelfHost
{
    class Program
    {
        public const int ApiPort = 9000;

        static int Main(string[] args)
        {
            ConfigureLogging();

            return (int)HostFactory.Run(x =>
            {
                x.UseSerilog();

                x.Service<OwinService>(s =>
                {
                    s.ConstructUsing(() => new OwinService());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });
            });

        }

        private static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("Application", "IA.Weather.API.SelfHost")
                .Enrich.FromLogContext()
                .WriteTo.LiterateConsole()
                .MinimumLevel.Debug()
                .Destructure.UsingAttributes()
                .CreateLogger();
        }
    }

    internal class OwinService
    {
        private IDisposable _webApp;

        public void Start()
        {
            var url = $"http://localhost:{Program.ApiPort}";
            var swaggerUrl = $"{url}/swagger";

            var options = new StartOptions();
            options.Urls.Add(url);

            _webApp = WebApp.Start<Startup>(options);

            Log.Information("API running: {url}", url);
            Log.Information("Swagger running: {url}", swaggerUrl);


#if DEBUG
            System.Diagnostics.Process.Start(swaggerUrl);
#endif
        }

        public void Stop()
        {
            _webApp.Dispose();
        }
    }

    internal class Startup : IA.Weather.API.Startup
    {
        protected override void Bindings(Container container)
        {
            Registrar.Register(container);
        }
    }
}
