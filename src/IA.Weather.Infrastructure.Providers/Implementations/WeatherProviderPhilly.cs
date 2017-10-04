using System;
using System.Threading.Tasks;
using IA.Weather.Domain.Models;
using IA.Weather.Infrastructure.Providers.Interfaces;

namespace IA.Weather.Infrastructure.Providers.Implementations
{
    public interface IWeatherProviderPhilly : IWeatherProvider { }

    //TODO: Test
    public class WeatherProviderPhilly : IWeatherProviderPhilly
    {
        public Task<WeatherModel> GetWeather(string country, string city)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException(nameof(country));
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException(nameof(city));

            if (city.Equals("Philadelphia", StringComparison.OrdinalIgnoreCase))
                return Task.FromResult(WeatherModel.New("Sunny"));

            return Task.FromResult(WeatherModel.New("Not Sunny"));

        }
    }
}
