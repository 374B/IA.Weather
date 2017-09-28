using System;
using System.Linq;
using IA.Weather.API.Bindings;
using Microsoft.Owin.Hosting;
using SimpleInjector;
using Topshelf;

namespace IA.Weather.API.SelfHost
{
    class Program
    {
        public const int ApiPort = 9000;
        public const int SeqPort = 9001;

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
            //var logCfg = new LogConfiguration();

            ////TODO: Logging configuration should be in the common startup? Should also be configuration based.
            //Log.Logger = new LoggerConfiguration()
            //    .Enrich.WithProperty("Application", "WeCare.Api.SelfHost")
            //    .Enrich.FromLogContext()
            //    .WriteTo.LiterateConsole()
            //    .WriteTo.Seq($"http://localhost:{SeqPort}", compact: true)
            //    .MinimumLevel.Debug()
            //    .Destructure.UsingAttributes()
            //    .CreateLogger();
        }
    }

    internal class OwinService
    {
        private IDisposable _webApp;

        public void Start()
        {
            var options = new StartOptions();
            options.Urls.Add($"http://localhost:{Program.ApiPort}");

            _webApp = WebApp.Start<Startup>(options);

#if DEBUG
            System.Diagnostics.Process.Start($"{options.Urls.First()}/swagger");
            //System.Diagnostics.Process.Start($"http://localhost:{Program.SeqPort}");
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
