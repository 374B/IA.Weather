using System;
using System.Net.Http;
using IA.Weather.Domain.Models;
using IA.Weather.Infrastructure.Providers.Interfaces;

namespace IA.Weather.Infrastructure.Providers.Implementations
{
    public interface IWeatherProviderOpenWeatherMap : IWeatherProvider { }

    public class WeatherProviderOpenWeatherMap : WeatherProviderHttp, IWeatherProviderOpenWeatherMap
    {
        private readonly string _apiKey;
        private readonly string _apiUrl;

        public WeatherProviderOpenWeatherMap(string apiKey, string apiUrl)
        {
            if (string.IsNullOrEmpty(apiKey)) throw new ArgumentException(nameof(apiKey));
            if (string.IsNullOrEmpty(apiUrl)) throw new ArgumentException(nameof(apiUrl));

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
            //TODO
           return WeatherModel.New(responseBody);
        }
    }
}
