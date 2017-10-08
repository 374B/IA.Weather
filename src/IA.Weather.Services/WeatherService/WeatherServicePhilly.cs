using System;
using System.Threading.Tasks;
using IA.Weather.Domain.Models;
using IA.Weather.Services.Contract.Interfaces;

namespace IA.Weather.Services.WeatherService
{
    public class WeatherServicePhilly : IWeatherService
    {
        public string Identifier => "PHI";
        public string Name => "Always Sunny In Philadelphia";
        public string Description => "The Weather is always sunny in Philadelphia";

        public Task<WeatherModel> GetByCity(string country, string city)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException(nameof(country));
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException(nameof(city));

            WeatherModel model;

            if (city.Equals("Philadelphia", StringComparison.OrdinalIgnoreCase))
            {
                model = new WeatherModel("Sunny", "It's always sunny in Philadelphia.");
            }
            else
            {
                model = new WeatherModel("Not Sunny", "It's not Philadelphia, so it's not sunny.");
            }

            return Task.FromResult(model);
        }
    }
}
