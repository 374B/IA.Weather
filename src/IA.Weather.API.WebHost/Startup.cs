using IA.Weather.API.Bindings;
using IA.Weather.API.WebHost;
using Microsoft.Owin;
using SimpleInjector;

[assembly: OwinStartup(typeof(Startup))]
namespace IA.Weather.API.WebHost
{
    public class Startup : API.Startup
    {
        protected override void Bindings(Container container)
        {
            Registrar.Register(container);
        }
    }
}