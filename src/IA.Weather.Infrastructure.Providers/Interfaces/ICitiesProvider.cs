using System.Collections.Generic;
using System.Threading.Tasks;

namespace IA.Weather.Infrastructure.Providers.Interfaces
{
    public interface ICitiesProvider
    {
        Task<List<string>> CitiesForCountry(string country);
    }
}
