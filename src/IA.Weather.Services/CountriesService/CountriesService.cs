using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IA.Weather.Infrastructure.Providers.Interfaces;
using IA.Weather.Services.Contract.Interfaces;

namespace IA.Weather.Services.CountriesService
{
    public class CountriesService : ICountriesService
    {
        private readonly ICountriesProvider _countriesProvider;
        private readonly ICitiesProvider _citiesProvider;

        private List<string> _countries;

        public CountriesService(ICountriesProvider countriesProvider, ICitiesProvider citiesProvider)
        {
            _countriesProvider = countriesProvider;
            _citiesProvider = citiesProvider;
        }

        public async Task<IEnumerable<string>> GetAllCountries()
        {
            //Countries aren't likely to change, only load them once and re-use
            if (_countries == null)
            {
                _countries = await _countriesProvider.GetCountries();
                _countries.Sort();
            }

            return _countries;

        }

        public async Task<IEnumerable<string>> GetCitiesForCountry(string country)
        {
            if (string.IsNullOrWhiteSpace(nameof(country))) throw new ArgumentNullException(nameof(country));

            var cities = await _citiesProvider.CitiesForCountry(country);
            return cities;
        }
    }
}
