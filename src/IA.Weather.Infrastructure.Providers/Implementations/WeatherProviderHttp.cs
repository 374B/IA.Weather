using System;
using System.Net.Http;
using System.Threading.Tasks;
using IA.Weather.Domain.Models;
using IA.Weather.Infrastructure.Providers.Interfaces;

namespace IA.Weather.Infrastructure.Providers.Implementations
{
    public abstract class WeatherProviderHttp : IWeatherProvider
    {
        public async Task<WeatherModel> GetWeather(string country, string city)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException(nameof(country));
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException(nameof(city));

            using (var req = CreateRequest(country, city))
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res;

                try
                {
                    res = await httpClient.SendAsync(req);
                    res.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    throw new Exception("An exception was thrown when executing the http request. See inner ex for details. " +
                        $"URL: {req.RequestUri}, Method: {req.Method}", ex);
                }

                try
                {
                    var responseBody = await res.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(responseBody))
                        throw new Exception("Response body was empty");

                    var model = MapResponse(responseBody);
                    return model;
                }
                catch (Exception ex)
                {
                    throw new Exception("An exception was thrown when mapping the response body to the model. See inner ex for details. " +
                        $"URL: {req.RequestUri}, Method: {req.Method}", ex);
                }

            }

        }

        protected abstract HttpRequestMessage CreateRequest(string country, string city);

        protected abstract WeatherModel MapResponse(string responseBody);

    }
}
