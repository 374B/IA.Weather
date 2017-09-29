using System.Linq;
using IA.Weather.Services;
using IA.Weather.Services.Contract;
using IA.Weather.Services.Contract.Interfaces;
using System.Reflection;
using IA.Weather.Services.WeatherService;
using SimpleInjector;

namespace IA.Weather.API.Bindings
{
    //TODO: Naming
    public static class Registrar
    {
        public static void Register(Container container)
        {
            //container.RegisterCollection<IWeatherService>(new[]
            //{
            //    typeof(WeatherServiceX),
            //    typeof(WeatherServiceOpenWeatherMap),
            //    typeof(WeatherServicePhilly)
            //});

            //The above commented code is replaced by auto registrations
            //... Why would you do this? So you don't need to modify this everytime you add a new IWeatherService implementation
            //... Why wouldn't you do this? May introduce unnecessary complexity

            var weatherServices =
                from type in typeof(WeatherServiceBase<,>).Assembly.GetExportedTypes()
                where 
                    !type.IsAbstract
                    && type.GetInterfaces().Any(x=>x== typeof(IWeatherService))
                select type;
            
            container.RegisterCollection<IWeatherService>(weatherServices);


        }
    }
}
