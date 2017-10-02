using System;
using System.Collections.Generic;
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
                Countries = countries.Select(x => new CountryResponse
                {
                    Name = x,
                    Links = new List<LinkResponse>
                    {
                        new LinkResponse
                        {
                            Rel = "cities",
                            Href = RouteGetCitiesByCountry(x)
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

            return Ok();
        }

        private string RouteGetCitiesByCountry(string country)
        {
            return this.Url.Link("GetCitiesByCountry", new { country });
        }


        //private string RouteWeatherByCountryAndCity()
        //{
        //    return new WeatherController().Url.Link("");
        //}

    }
}
