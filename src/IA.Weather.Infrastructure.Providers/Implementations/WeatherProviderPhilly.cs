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
            return Task.FromResult(WeatherModel.New("Sunny"));
        }
    }
}
