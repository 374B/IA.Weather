using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using IA.Weather.Domain.Models;
using IA.Weather.Services.Contract.Interfaces;

namespace IA.Weather.API.Controllers
{
    [RoutePrefix("api")]
    public class WeatherController : ApiController
    {
        private readonly IEnumerable<IWeatherService> _weatherServices;

        public WeatherController(IEnumerable<IWeatherService> weatherServices)
        {
            _weatherServices = weatherServices.ToList();
        }

        [HttpGet]
        [Route("services")]
        public IHttpActionResult AvailableServices()
        {
            //NOTE: One of the concepts you'd usually want to see here is keeping your external contract insulated from your code changes
            //... Generally I dislike using dynamic classes, but why not
            //... A dynamic *could* be used only if you name the props, otherwise code changes would change the response data
            //... (even if intellisense says they are redundant)
            var services = _weatherServices.Select(x => new
            {
                Identifier = x.Identifier,
                Name = x.Name,
                Description = x.Description
            })
            .ToList();


            //TODO Restful URLs?

            //I also generally prefer to have a parent object, as this is more open to non-breaking modifications in the future
            //... If this was production code you would be writing DTO classes or Response classes 
            //... But its not, so dynamics again

            var responseObj = new
            {
                Services = services,
                Meta = new
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
                //TODO
                throw;
            }
        }

        [HttpGet]
        [Route("services/{service}/weather")]
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
                //TODO
                throw;
            }

        }
    }
}

