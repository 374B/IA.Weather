using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using IA.Weather.API.DTO.Responses;
using IA.Weather.API.Helpers;
using IA.Weather.Domain.Models;
using IA.Weather.Services.Contract.Interfaces;
using Serilog;

namespace IA.Weather.API.Controllers
{
    [RoutePrefix("api/weather")]
    public class WeatherController : ApiController
    {
        private readonly IRouteProvider _routeProvider;
        private readonly IEnumerable<IWeatherService> _weatherServices;

        public WeatherController(
            IRouteProvider routeProvider,
            IEnumerable<IWeatherService> weatherServices)
        {
            _routeProvider = routeProvider;
            _weatherServices = weatherServices.ToList();
        }

        [HttpGet]
        [Route("services")]
        public IHttpActionResult AvailableServices()
        {
            var services = _weatherServices.Select(x => new WeatherServiceResponse
            {
                Identifier = x.Identifier,
                Name = x.Name,
                Description = x.Description,
                Link = _routeProvider.Route(this, "WeatherFromService", new { service = x.Identifier })
            })
            .ToList();

            var responseObj = new WeatherServicesResponse
            {
                Services = services,
                Meta = new WeatherServicesResponseMeta
                {
                    Count = services.Count
                }
            };

            return Ok(responseObj);
        }

        [HttpGet]
        [Route("services/weather")]
        public async Task<IHttpActionResult> WeatherFromAllServices([FromUri] string country, [FromUri] string city)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentNullException(nameof(country));
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentNullException(nameof(city));

            var tasks = _weatherServices.Select(x =>
           {
               return new Func<Task<KeyValuePair<string, WeatherModel>>>(async () =>
                {
                    var result = await x.GetByCity(country, city);
                    return new KeyValuePair<string, WeatherModel>(x.Identifier, result);
                })();
           });

            try
            {
                var results = await Task.WhenAll(tasks);
                var responseObj = new
                {
                    Results = results.Select(x => new
                    {
                        x.Key,
                        x.Value.Weather
                    })
                };

                return Ok(responseObj);

            }
            catch (Exception ex)
            {
                Log.Error(ex, "WeatherFromAllServices failed. Country : {country}, City: {city}", country, city);
                throw;
            }
        }

        [HttpGet]
        [Route("services/{service}/weather", Name = "WeatherFromService")]
        public async Task<IHttpActionResult> WeatherFromService([FromUri]string service, [FromUri] string country, [FromUri] string city)
        {
            if (string.IsNullOrWhiteSpace(service)) throw new ArgumentNullException(nameof(service));
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentNullException(nameof(country));
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentNullException(nameof(city));

            var targetService = _weatherServices.FirstOrDefault(x => x.Identifier.Equals(service, StringComparison.OrdinalIgnoreCase));
            if (targetService == null) return BadRequest($"Could not find a service matching the specified service identifier '{service}'");

            try
            {
                var model = await targetService.GetByCity(country, city);
                return Ok(model.Weather);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "WeatherFromService failed. Country : {country}, City: {city}", country, city);
                throw;
            }

        }


    }
}

