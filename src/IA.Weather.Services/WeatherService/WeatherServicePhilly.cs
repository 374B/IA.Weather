using System;
using System.Threading.Tasks;
using IA.Weather.Domain.Models;

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

    public interface IWeatherProviderPhilly : IWeatherProvider { }

    public class WeatherProviderPhilly : IWeatherProviderPhilly
    {
        public Task<WeatherModel> GetWeatherResponse(WeatherRequest request)
        {
            return Task.FromResult(WeatherModel.New("Sunny"));
        }
    }
}
