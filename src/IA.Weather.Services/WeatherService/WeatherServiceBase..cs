using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IA.Weather.Domain.Models;
using IA.Weather.Services.Contract.Interfaces;

namespace IA.Weather.Services.WeatherService
{
    //TODO: Tidy up the classes at the bottom
    public abstract class WeatherServiceBase : IWeatherService
    {
        private readonly IWeatherProvider _provider;

        public abstract string Identifier { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }

        protected WeatherServiceBase(IWeatherProvider provider)
        {
            _provider = provider;
        }

        public virtual async Task<WeatherModel> GetByCountry(string country)
        {
            var req = new WeatherRequest();
            WeatherModel model;

            try
            {
                model = await _provider.GetWeatherResponse(req);
            }
            catch (Exception ex)
            {
                //TODO: Log
                model = WeatherModel.FromException(ex);
            }

            return model;
        }

        public virtual async Task<List<WeatherModel>> GetByCountries(List<string> countries)
        {
            var tasks = countries.Select(GetByCountry);
            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }

    }

    public class WeatherResponse
    {

    }

    public class WeatherRequest
    {

    }

    public interface IWeatherProvider
    {
        Task<WeatherModel> GetWeatherResponse(WeatherRequest request);
    }

}
