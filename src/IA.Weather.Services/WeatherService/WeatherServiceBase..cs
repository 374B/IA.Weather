using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IA.Weather.Domain.Models;
using IA.Weather.Services.Contract.Interfaces;

namespace IA.Weather.Services.WeatherService
{
    public class SomeResult { }

    public abstract class WeatherServiceBase<TWeatherProvider, TWeatherProviderResult> : IWeatherService
        where TWeatherProvider : IWeatherProvider<TWeatherProviderResult>
    {
        private readonly TWeatherProvider _provider;

        public abstract string Identifier { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }

        protected WeatherServiceBase(TWeatherProvider provider)
        {
            _provider = provider;
        }

        public virtual async Task<WeatherModel> GetByCountry(string country)
        {
            var req = new WeatherRequest();
            WeatherModel model;

            try
            {
                var result = await _provider.GetWeatherResponse(req);
                //model = _mapper.Map(result);
                model = null;
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

        //protected abstract WeatherModel MapResult(TWeatherProviderResult result);

    }

    public class WeatherResponse
    {

    }

    public class WeatherRequest
    {

    }

    public interface IWeatherProvider<TResponse>
    {
        Task<TResponse> GetWeatherResponse(WeatherRequest request);
    }

}
