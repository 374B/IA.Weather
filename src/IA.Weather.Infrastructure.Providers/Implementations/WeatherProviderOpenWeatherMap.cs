using System;
using System.Threading.Tasks;
using IA.Weather.Domain.Models;
using IA.Weather.Infrastructure.Providers.Interfaces;

namespace IA.Weather.Infrastructure.Providers.Implementations
{
    public interface IWeatherProviderOpenWeatherMap : IWeatherProvider { }

    public class WeatherProviderOpenWeatherMap : IWeatherProviderOpenWeatherMap
    {
        public Task<WeatherModel> GetWeather(string country, string city)
        {
            throw new NotImplementedException();
        }
    }
}
