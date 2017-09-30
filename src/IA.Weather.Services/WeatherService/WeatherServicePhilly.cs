using IA.Weather.Infrastructure.Providers.Implementations;

namespace IA.Weather.Services.WeatherService
{
    public class WeatherServicePhilly : WeatherServiceBase
    {
        public override string Identifier => "PHI";
        public override string Name => "Always Sunny In Philadelphia";
        public override string Description => "The Weather is always sunny in Philadelphia";

        public WeatherServicePhilly(IWeatherProviderPhilly provider) : base(provider)
        {
        }
    }


}
