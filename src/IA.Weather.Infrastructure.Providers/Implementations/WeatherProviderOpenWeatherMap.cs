using System;
using System.Net.Http;
using IA.Weather.Domain.Factories;
using IA.Weather.Domain.Models;
using IA.Weather.Infrastructure.Providers.Interfaces;
using Newtonsoft.Json;

namespace IA.Weather.Infrastructure.Providers.Implementations
{
    public interface IWeatherProviderOpenWeatherMap : IWeatherProvider { }

    public class WeatherProviderOpenWeatherMap : WeatherProviderHttp, IWeatherProviderOpenWeatherMap
    {
        private readonly ITemperatureModelFactory _temperatureModelFactory;
        private readonly string _apiKey;
        private readonly string _apiUrl;

        public WeatherProviderOpenWeatherMap(ITemperatureModelFactory temperatureModelFactory, string apiKey, string apiUrl)
        {
            if (temperatureModelFactory == null) throw new ArgumentNullException(nameof(temperatureModelFactory));
            if (string.IsNullOrEmpty(apiKey)) throw new ArgumentException(nameof(apiKey));
            if (string.IsNullOrEmpty(apiUrl)) throw new ArgumentException(nameof(apiUrl));

            _temperatureModelFactory = temperatureModelFactory;
            _apiKey = apiKey;
            _apiUrl = apiUrl;
        }

        protected override HttpRequestMessage CreateRequest(string country, string city)
        {
            var url = _apiUrl + $"?q={city}&APPID={_apiKey}";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            return req;
        }

        protected override WeatherModel MapResponse(string responseBody)
        {
            var jObj = JsonConvert.DeserializeObject<dynamic>(responseBody);

            var weather = jObj.weather[0];
            var main = jObj.main;
            var wind = jObj.wind;

            var desc1 = (string)weather.main;
            var desc2 = (string)weather.description;
            var tempMin = _temperatureModelFactory.FromKelvin((decimal)main.temp_min);
            var tempMax = _temperatureModelFactory.FromKelvin((decimal)main.temp_max);
            var tempCurrent = _temperatureModelFactory.FromKelvin((decimal)main.temp);
            var humidity = (decimal?)main.humidity;
            var pressure = (decimal?)main.pressure;
            var windSpeed = (decimal?)wind.speed;
            var windDeg = (decimal?)wind.deg;

            var model = new WeatherModel(
                desc1, desc2,
                tempMin, tempMax, tempCurrent,
                pressure, humidity,
                windDeg, windSpeed);

            return model;

        }
    }
}
