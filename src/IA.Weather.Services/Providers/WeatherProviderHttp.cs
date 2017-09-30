using System;
using System.Net.Http;
using System.Threading.Tasks;
using IA.Weather.Domain.Models;
using IA.Weather.Services.WeatherService;

namespace IA.Weather.Services.Providers
{
    public abstract class WeatherProviderHttp : IWeatherProvider
    {
        protected abstract HttpRequestMessage CreateRequest();

        protected abstract WeatherModel MapResponse(string responseBody);

        public async Task<WeatherModel> GetWeatherResponse(WeatherRequest request)
        {
            var req = CreateRequest();

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
    }
}
