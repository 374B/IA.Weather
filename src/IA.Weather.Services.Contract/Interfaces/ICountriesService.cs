using System.Collections.Generic;
using System.Threading.Tasks;

namespace IA.Weather.Services.Contract.Interfaces
{
    public interface ICountriesService
    {
        Task<IEnumerable<string>> GetAllCountries();

        Task<IEnumerable<string>> GetCitiesForCountry(string country);
    }
}
