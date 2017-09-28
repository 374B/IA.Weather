using IA.Weather.Services.Contract;
using System.Web.Http;

namespace IA.Weather.API.Controllers
{
    public class WeatherController : ApiController
    {
        private readonly IEchoService _echoService;

        public WeatherController(IEchoService echoService)
        {
            _echoService = echoService;
        }

        public IHttpActionResult Echo(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return BadRequest($"{nameof(value)} is required");
            
            var echoed = _echoService.Echo(value);

            return Ok(echoed);
        }
    }
}
