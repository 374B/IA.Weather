using IA.Weather.Services;
using IA.Weather.Services.Contract;
using SimpleInjector;

namespace IA.Weather.API.Bindings
{
    //TODO: Naming
    public static class Registrar
    {
        public static void Register(Container container)
        {
            container.Register<IEchoService, EchoService>(Lifestyle.Singleton);
        }
    }
}
