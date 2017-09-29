using System.Threading.Tasks;
using IA.Weather.Domain.Models;

namespace IA.Weather.Services.Contract.Interfaces
{
    public interface IWeatherService
    {
        string Identifier { get; }
        string Name { get; }
        string Description { get; }

        Task<WeatherModel> GetByCountry(string country);
    }
}
