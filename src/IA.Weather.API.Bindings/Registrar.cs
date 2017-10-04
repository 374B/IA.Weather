using System;
using System.Collections.Generic;
using System.Linq;
using IA.Weather.Services.Contract.Interfaces;
using IA.Weather.Services.CountriesService;
using IA.Weather.Services.WeatherService;
using SimpleInjector;
using System.Configuration;
using IA.Weather.Infrastructure.Providers.Helpers;
using IA.Weather.Infrastructure.Providers.Implementations;
using IA.Weather.Infrastructure.Providers.Interfaces;
using IA.Weather.API.Helpers;

namespace IA.Weather.API.Bindings
{
    public static class Registrar
    {
        public static void Register(Container container)
        {
            WeatherServices(container);
            WeatherProviders(container);
            CountriesProviders(container);

            container.Register<ICountriesService, CountriesService>(Lifestyle.Singleton);

            container.Register<ICitiesProvider, CitiesProviderX>(Lifestyle.Singleton);

            container.Register(() =>
            {
                var endpoint = "webservicex:endpoint".GetAppSetting(Required);
                return new GlobalWeatherServiceProxy(endpoint);
            },
            Lifestyle.Singleton);

            container.Register<IRouteProvider, RouteProvider>(Lifestyle.Singleton);

        }

        private static void WeatherServices(Container container)
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

        }

        private static void WeatherProviders(Container container)
        {
            container.Register<IWeatherProviderX, WeatherProviderX>(Lifestyle.Singleton);

            container.Register<IWeatherProviderOpenWeatherMap>(() =>
            {
                var apiKey = "openweathermap.org:apiKey".GetAppSetting(Required);
                var apiUrl = "openweathermap.org:apiUrl".GetAppSetting(Required);

                return new WeatherProviderOpenWeatherMap(apiKey, apiUrl);

            }, Lifestyle.Singleton);

            container.Register<IWeatherProviderPhilly, WeatherProviderPhilly>(Lifestyle.Singleton);
        }

        private static void CountriesProviders(Container container)
        {
            //container.Register<ICountriesProvider, CountriesProviderFromCulture>(Lifestyle.Singleton);

            container.Register<ICountriesProvider>(() =>
            {
                var url = "restcountries.eu:url:allcountries".GetAppSetting(Required);
                return new CountriesProviderRestCountriesEu(url);

            }, Lifestyle.Singleton);
        }

        #region Helper methods that definitely do not belong here...

        /// <summary>
        /// Loads the app setting value from the config
        /// </summary>
        /// <param name="key">The key of the app setting</param>
        /// <param name="validation">A function that will validate the value and return a list of errors if invalid</param>
        /// <returns></returns>
        private static string GetAppSetting(this string key, Func<string, List<string>> validation = null)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (validation == null) return value;

            var errors = validation(value);
            if (errors != null && errors.Any())
                throw new ConfigurationErrorsException($"App setting validation failed for {nameof(key)}. Validation errors: \n{string.Join("\n", errors)}");

            return value;
        }

        private static List<string> Required(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new List<string> { "Value can not be null or whitespace" };

            return null;
        }

        #endregion

    }
}
