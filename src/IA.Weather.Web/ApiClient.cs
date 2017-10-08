using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using IA.Weather.API.DTO.Responses;
using Newtonsoft.Json;
using Serilog;

namespace IA.Weather.Web
{
    public interface IApiClient
    {
        Task<CountriesResponse> GetCountriesList();

        Task<WeatherServicesResponse> GetWeatherServicesList();
    }

    public class ApiClient : IApiClient
    {
        private readonly ILogger _logger;
        private readonly string _hostUrl;

        public ApiClient(ILogger logger)
        {
            _logger = logger;

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
            var requestLogger = _logger.ForContext("ApiClient - Request", Guid.NewGuid().ToString("D"));

            try
            {
                requestLogger.Information("ApiClient - Request start: Method: {method}, URL: {url}", msg.Method,
                    msg.RequestUri);

                return await GetResponseInner<T>(requestLogger, msg);

            }
            catch (Exception ex)
            {
                requestLogger.Error(ex, "An exception occurred");

                throw new Exception(
                    $"An exception occurred for request [{msg.Method}] '{msg.RequestUri}'. See inner ex for details",
                    ex);
            }
        }

        private async Task<T> GetResponseInner<T>(ILogger requestLogger, HttpRequestMessage msg)
        {
            using (var httpClient = new HttpClient())
            {
                var res = await httpClient.SendAsync(msg);

                requestLogger.Information("ApiClient - Response received. Status codde: {statusCode}", res.StatusCode);
                
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
                    requestLogger.Error(ex, "An exception occurred during deserialization");
                    throw new Exception($"Could not deserialize response content into {typeof(T).Name}. See inner ex for details", ex);
                }
            }
        }
    }
}