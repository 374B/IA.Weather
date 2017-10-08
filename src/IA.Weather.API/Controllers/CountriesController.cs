using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using IA.Weather.API.DTO.Responses;
using IA.Weather.API.Helpers;
using IA.Weather.Services.Contract.Interfaces;

namespace IA.Weather.API.Controllers
{
    [RoutePrefix("api")]
    public class CountriesController : ApiController
    {
        private readonly IRouteProvider _routeProvider;
        private readonly ICountriesService _countriesService;

        public CountriesController(
            IRouteProvider routeProvider,
            ICountriesService countriesService)
        {
            _routeProvider = routeProvider;
            _countriesService = countriesService;
        }

        [HttpGet]
        [Route("countries")]
        public async Task<IHttpActionResult> GetAllCountries()
        {
            var countries = await _countriesService.GetAllCountries();

            var res = new CountriesResponse
            {
                Countries = countries.Select(x => new CountryResponse
                {
                    Name = x,
                    Links = new List<LinkResponse>
                    {
                        new LinkResponse
                        {
                            Rel = "cities",
                            Href = _routeProvider.Route(this, "GetCitiesByCountry", new { country = x })
                        }
                    }
                }).ToList()
            };

            return Ok(res);
        }

        [HttpGet]
        [Route("country/{country}/cities", Name = "GetCitiesByCountry")]
        public async Task<IHttpActionResult> GetCitiesByCountry([FromUri]string country)
        {
            if (string.IsNullOrWhiteSpace(country)) return BadRequest($"{nameof(country)} is required");

            var res = await _countriesService.GetCitiesForCountry(country);

            //TODO: DTO

            return Ok(res);
        }

    }
}
