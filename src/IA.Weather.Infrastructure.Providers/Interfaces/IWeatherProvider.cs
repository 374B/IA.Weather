using System.Threading.Tasks;
using IA.Weather.Domain.Models;

namespace IA.Weather.Infrastructure.Providers.Interfaces
{
    public interface IWeatherProvider
    {
        Task<WeatherModel> GetWeather(string country, string city);
    }
}
