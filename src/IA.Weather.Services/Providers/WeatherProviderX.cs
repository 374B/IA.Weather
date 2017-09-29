using System.Net.Http;
using IA.Weather.Domain.Models;
using IA.Weather.Services.WeatherService;

namespace IA.Weather.Services.Providers
{
    public class WeatherProviderX : HttpWeatherProvider, IWeatherProviderX
    {
        protected override HttpRequestMessage CreateRequest()
        {
            var req = new HttpRequestMessage(
                HttpMethod.Get, 
                "http://www.webservicex.net/globalweather.asmx/GetWeather?CityName=Sydney&CountryName=Australia");

            return req;
        }

        protected override WeatherModel MapResponse(string responseBody)
        {
            return WeatherModel.New(responseBody);
        }
    }
}
