using System;
using System.Threading.Tasks;
using IA.Weather.Domain.Models;

namespace IA.Weather.Services.WeatherService
{
    public class WeatherServiceOpenWeatherMap : WeatherServiceBase
    {
        public override string Identifier => "OWM";
        public override string Name => "Open Weather Map";
        public override string Description => "Weather served from http://api.openweathermap.org/";

        public WeatherServiceOpenWeatherMap(IWeatherProviderOpenWeatherMap provider) : base(provider)
        {
        }
    }

    public interface IWeatherProviderOpenWeatherMap : IWeatherProvider { }

    public class WeatherProviderOpenWeatherMap : IWeatherProviderOpenWeatherMap
    {
        public Task<WeatherModel> GetWeatherResponse(WeatherRequest request)
        {
            throw new NotImplementedException();
        }
    }

}
