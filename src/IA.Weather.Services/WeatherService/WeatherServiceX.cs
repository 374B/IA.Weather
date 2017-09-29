using System;
using System.Threading.Tasks;

namespace IA.Weather.Services.WeatherService
{
    public class WeatherServiceX : WeatherServiceBase<WeatherProviderX, SomeResult>
    {
        public override string Identifier => "X";
        public override string Name => "Webservicex.net";
        public override string Description => "Weather Served from Webservicex.net";

        public WeatherServiceX(WeatherProviderX provider) : base(provider)
        {
        }
    }

    public class WeatherProviderX : IWeatherProvider<SomeResult>
    {
      public Task<SomeResult> GetWeatherResponse(WeatherRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
