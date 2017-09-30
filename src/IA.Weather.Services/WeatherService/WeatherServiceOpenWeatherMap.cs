using IA.Weather.Infrastructure.Providers.Implementations;

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
}
