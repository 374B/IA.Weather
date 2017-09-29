using System;
using System.Threading.Tasks;

namespace IA.Weather.Services.WeatherService
{
    public class WeatherServiceOpenWeatherMap : WeatherServiceBase<WeatherProviderOpenWeather, SomeResult>
    {
        public override string Identifier => "OWM";
        public override string Name => "Open Weather Map";
        public override string Description => "Weather served from http://api.openweathermap.org/";

        public WeatherServiceOpenWeatherMap(WeatherProviderOpenWeather provider) : base(provider)
        {
        }

    }

    public class WeatherProviderOpenWeather : IWeatherProvider<SomeResult>
    {
        public Task<SomeResult> GetWeatherResponse(WeatherRequest request)
        {
            throw new NotImplementedException();
        }
    }

}
