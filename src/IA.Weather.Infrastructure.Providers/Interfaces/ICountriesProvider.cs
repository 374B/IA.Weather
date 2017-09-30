using System.Collections.Generic;
using System.Threading.Tasks;

namespace IA.Weather.Infrastructure.Providers.Interfaces
{
    public interface ICountriesProvider
    {
        Task<List<string>> GetCountries();
    }
}
