using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using IA.Weather.API.DTOs.Responses;
using IA.Weather.Services.Contract.Interfaces;

namespace IA.Weather.API.Controllers
{
    //TODO: Default prefix?
    [RoutePrefix("api")]
    public class CountriesController : ApiController
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        [HttpGet]
        [Route("countries")]
        public async Task<IHttpActionResult> GetAllCountries()
        {
            //TODO: Ex handling

            var countries = await _countriesService.GetAllCountries();

            var res = new CountriesResponse
            {
                Countries = countries.ToList()
            };

            return Ok(res);
        }
        
        [HttpGet]
        [Route("country/{country}/cities")]
        public async Task<IHttpActionResult> GetCitiesByCountry([FromUri]string country)
        {
            if (string.IsNullOrWhiteSpace(country)) return BadRequest($"{nameof(country)} is required");

            return Ok();
        }

    }
}
