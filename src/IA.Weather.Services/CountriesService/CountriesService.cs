using System.Collections.Generic;
using System.Threading.Tasks;
using IA.Weather.Infrastructure.Providers.Interfaces;
using IA.Weather.Services.Contract.Interfaces;

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

        public async Task<IEnumerable<string>> GetAllCountries()
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
