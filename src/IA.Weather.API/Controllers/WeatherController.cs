using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
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
    }
}
