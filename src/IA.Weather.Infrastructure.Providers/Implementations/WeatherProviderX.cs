using System;
using System.Threading.Tasks;
using IA.Weather.Domain.Models;
using IA.Weather.Infrastructure.Providers.Helpers;
using IA.Weather.Infrastructure.Providers.Interfaces;

namespace IA.Weather.Infrastructure.Providers.Implementations
{
    public interface IWeatherProviderX : IWeatherProvider { }

    public class WeatherProviderX : IWeatherProviderX
    {
        private readonly GlobalWeatherServiceProxy _serviceProxy;

        public WeatherProviderX(GlobalWeatherServiceProxy serviceProxy)
        {
            _serviceProxy = serviceProxy;
        }

        public async Task<WeatherModel> GetWeather(string country, string city)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException(nameof(country));
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException(nameof(city));

            var data = await _serviceProxy.Invoke(c => c.GetWeatherAsync(city, country));

            var mapped = MapData(data);

            return mapped;

        }

        private WeatherModel MapData(string data)
        {
            if (string.Equals("Data not found", data, StringComparison.OrdinalIgnoreCase))
                return WeatherModel.FromException(new Exception("No data found"));

            //TODO: Never got a succesful response back
            //... Happy path not done
            throw new NotImplementedException();
            
        }

    }
}
