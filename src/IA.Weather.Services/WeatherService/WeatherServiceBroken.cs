using System;
using System.Threading.Tasks;
using IA.Weather.Domain.Models;
using IA.Weather.Services.Contract.Interfaces;

namespace IA.Weather.Services.WeatherService
{
    public class WeatherServiceBroken : IWeatherService
    {
        public string Identifier => "BRK";
        public string Name => "Broken";
        public string Description => "This weather service will always return an error";

        public Task<WeatherModel> GetByCity(string country, string city)
        {
            return Task.FromResult(WeatherModel.FromException(new Exception("An expected error has occurred")));
        }
    }
}
