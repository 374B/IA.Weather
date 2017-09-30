using System.Collections.Generic;
using System.Threading.Tasks;
using IA.Weather.Services.Contract.Interfaces;
using IA.Weather.Services.Providers;

namespace IA.Weather.Services.CountriesService
{
    public class CountriesService : ICountriesService
    {
        private readonly ICountriesProvider _provider;

        private List<string> _countries;

        public CountriesService(ICountriesProvider provider)
        {
            _provider = provider;
        }

        public async Task<List<string>> GetAllCountries()
        {
            //Countries aren't likely to change, only load them once and re-use
            if (_countries == null)
            {
                _countries = await _provider.GetCountries();
                _countries.Sort();
            }

            return _countries;

        }
    }
}
