using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using IA.Weather.API.DTO.Responses;
using Newtonsoft.Json;

namespace IA.Weather.Web
{
    public interface IApiClient
    {
        Task<CountriesResponse> GetCountriesList();

        Task<WeatherServicesResponse> GetWeatherServicesList();
    }

    public class ApiClient
    {
        private readonly string _hostUrl;

        public ApiClient()
        {
            var key = "ia.weather.api:hosturl";

            _hostUrl = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrWhiteSpace(_hostUrl)) throw new ConfigurationErrorsException($"Required app setting not found: {key}");

            if (!_hostUrl.EndsWith("/"))
                _hostUrl = _hostUrl + "/";

        }

        public async Task<CountriesResponse> GetCountriesList()
        {
            var url = _hostUrl + "api/countries";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            var res = await GetResponse<CountriesResponse>(req);

            return res;
        }

        public async Task<WeatherServicesResponse> GetWeatherServicesList()
        {
            var url = _hostUrl + "api/weather/services";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            var res = await GetResponse<WeatherServicesResponse>(req);

            return res;
        }

        private async Task<T> GetResponse<T>(HttpRequestMessage msg)
        {
            try
            {
                //TODO: Log req start
                return await GetResponseInner<T>(msg);
                //TODO: Log req success
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"An exception occurred for request [{msg.Method}] '{msg.RequestUri}'. See inner ex for details",
                    ex);
            }
            finally
            {
                //TODO: Log req end
            }
        }

        private async Task<T> GetResponseInner<T>(HttpRequestMessage msg)
        {
            using (var httpClient = new HttpClient())
            {
                var res = await httpClient.SendAsync(msg);
                res.EnsureSuccessStatusCode();

                if (res.Content == null) throw new NullReferenceException("Content was null");

                var content = await res.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(content)) throw new NullReferenceException("Content was null or empty");

                try
                {
                    var obj = JsonConvert.DeserializeObject<T>(content);
                    return obj;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Could not deserialize response content into {typeof(T).Name}. See inner ex for details", ex);
                }
            }
        }
    }
}