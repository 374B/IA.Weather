using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IA.Weather.Domain.Models;
using IA.Weather.Services.Contract.Interfaces;
using IA.Weather.Infrastructure.Providers.Interfaces;
using Serilog;

namespace IA.Weather.Services.WeatherService
{
    //TODO: Can we make this non-abstract and refactor so we don't need all the service implementations
    public abstract class WeatherServiceBase : IWeatherService
    {
        private readonly IWeatherProvider _provider;

        public abstract string Identifier { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }

        protected WeatherServiceBase(IWeatherProvider provider)
        {
            _provider = provider;
        }

        public virtual async Task<WeatherModel> GetByCity(string country, string city)
        {
            WeatherModel model;

            try
            {
                model = await _provider.GetWeather(country, city);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GetByCity failed. Country : {country}, City: {city}", country, city);
                model = WeatherModel.FromException(ex);
            }

            return model;
        }

        public virtual async Task<List<WeatherModel>> GetByCountries(List<KeyValuePair<string, string>> countriesAndCities)
        {
            //Should use a class instead of leveraging KVP

            var tasks = countriesAndCities.Select(kvp => GetByCity(kvp.Key, kvp.Value));
            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }

    }
}
