using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IA.Weather.Domain.Models;
using IA.Weather.Infrastructure.Providers.Implementations;
using IA.Weather.Infrastructure.Providers.Interfaces;
using IA.Weather.Services.Contract.Interfaces;
using Serilog;

namespace IA.Weather.Services.WeatherService
{
    public class WeatherServiceReliable : IWeatherService
    {
        private readonly ILogger _logger;
        private readonly List<IWeatherProvider> _providers;

        public string Identifier => "REL";
        public string Name => "Reliable";
        public string Description => "A more reliable weather service";

        public WeatherServiceReliable(
            ILogger logger,
            IWeatherProviderOpenWeatherMap providerOpenWeatherMap,
            IWeatherProviderX providerX)
        {
            _logger = logger;
            _providers = new List<IWeatherProvider> { providerOpenWeatherMap, providerX };
        }

        public async Task<WeatherModel> GetByCity(string country, string city)
        {
            var errors = new List<ErrorModel>();

            foreach (var provider in _providers)
            {
                try
                {
                    var res = await provider.GetWeather(country, city);

                    if (!res.Errored)
                        return res;

                    errors.Add(res.Error);
                }
                catch (Exception ex)
                {
                    _logger.Warning(ex, "Provider exception. Type: {providerType}, Method: {method}", provider.GetType().Name, nameof(provider.GetWeather));
                    errors.Add(ErrorModel.FromException(ex, $"Exception from {provider.GetType().Name}"));
                }
            }

            //If we have reached here nothing was returned

            var errorMessage = $"One or more errors occurred:\n{string.Join("\n", errors.Select(e => e.ErrorMessage))}";
            var exceptions = errors.Select(e => e.Exception);

            return WeatherModel.FromException(new AggregateException(errorMessage, exceptions));

        }
    }
}

