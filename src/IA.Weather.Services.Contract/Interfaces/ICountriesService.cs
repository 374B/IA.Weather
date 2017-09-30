using System.Collections.Generic;
using System.Threading.Tasks;

namespace IA.Weather.Services.Contract.Interfaces
{
    public interface ICountriesService
    {
        Task<List<string>> GetAllCountries();
    }
}
