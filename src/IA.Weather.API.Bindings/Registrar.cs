using System.Linq;
using IA.Weather.Services.Contract.Interfaces;
using IA.Weather.Services.CountriesService;
using IA.Weather.Services.WeatherService;
using SimpleInjector;
using System.Configuration;
using IA.Weather.Infrastructure.Providers.Implementations;
using IA.Weather.Infrastructure.Providers.Interfaces;

namespace IA.Weather.API.Bindings
{
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
                from type in typeof(WeatherServiceBase).Assembly.GetExportedTypes()
                where
                    !type.IsAbstract
                    && type.GetInterfaces().Any(x => x == typeof(IWeatherService))
                select type;

            container.RegisterCollection<IWeatherService>(weatherServices);

            //...

            container.Register<IWeatherProviderX, WeatherProviderX>(Lifestyle.Singleton);
            container.Register<IWeatherProviderOpenWeatherMap, WeatherProviderOpenWeatherMap>(Lifestyle.Singleton);
            container.Register<IWeatherProviderPhilly, WeatherProviderPhilly>(Lifestyle.Singleton);

            container.Register<ICountriesService, CountriesService>(Lifestyle.Singleton);

            //container.Register<ICountriesProvider, CountriesProviderFromCulture>(Lifestyle.Singleton);

            container.Register<ICountriesProvider>(() =>
            {
                const string key = "restcountries.eu:url:allcountries";
                var url = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrWhiteSpace(url)) throw new ConfigurationErrorsException($"Required app setting not found or not set. Key: {key}");

                return new CountriesProviderRestCountriesEu(url);

            }, Lifestyle.Singleton);

            container.Register<ICitiesProvider>(() =>
            {
                const string key = "webservicex:endpoint";
                var endpoint = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrWhiteSpace(endpoint)) throw new ConfigurationErrorsException($"Required app setting not found or not set. Key: {key}");

                return new CitiesProviderX(endpoint);

            }, Lifestyle.Singleton);

        }
    }
}
