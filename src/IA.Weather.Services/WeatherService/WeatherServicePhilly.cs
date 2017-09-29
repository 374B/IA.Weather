using System;
using System.Threading.Tasks;

namespace IA.Weather.Services.WeatherService
{
    public class WeatherServicePhilly : WeatherServiceBase<WeatherProviderAlwaysSunnyInPhiladelphia, SomeResult>
    {
        public override string Identifier => "PHI";
        public override string Name => "Always Sunny In Philadelphia";
        public override string Description => "The Weather is always sunny in Philadelphia";

        public WeatherServicePhilly(WeatherProviderAlwaysSunnyInPhiladelphia provider) : base(provider)
        {
        }
    }

    public class WeatherProviderAlwaysSunnyInPhiladelphia : IWeatherProvider<SomeResult>
    {
        public string Name => "Always Sunny In Philadelphia";

        public Task<SomeResult> GetWeatherResponse(WeatherRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
