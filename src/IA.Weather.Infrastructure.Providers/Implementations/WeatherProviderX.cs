using System.Net.Http;
using IA.Weather.Domain.Models;
using IA.Weather.Infrastructure.Providers.Interfaces;

namespace IA.Weather.Infrastructure.Providers.Implementations
{
    //TODO: Can we remove this
    public interface IWeatherProviderX : IWeatherProvider { }

    public class WeatherProviderX : WeatherProviderHttp, IWeatherProviderX
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
