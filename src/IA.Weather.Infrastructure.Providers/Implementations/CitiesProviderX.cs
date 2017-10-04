using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml.Linq;
using IA.Weather.Infrastructure.Providers.Helpers;
using IA.Weather.Infrastructure.Providers.Interfaces;

namespace IA.Weather.Infrastructure.Providers.Implementations
{
    public class CitiesProviderX : ICitiesProvider
    {
        private readonly GlobalWeatherServiceProxy _serviceProxy;

        public CitiesProviderX(GlobalWeatherServiceProxy serviceProxy)
        {
            _serviceProxy = serviceProxy;
        }

        public async Task<List<string>> CitiesForCountry(string country)
        {
            var results = new List<string>();

            var data = await _serviceProxy.Invoke(c => c.GetCitiesByCountryAsync(country));

            using (var tr = new StringReader(data))
            {
                var xdoc = XDocument.Load(tr);
                var cityElements = xdoc.Descendants("City").ToList();

                if (cityElements.Any())
                    results.AddRange(cityElements.Select(e => e.Value.Trim()));

                return results;
                
            }
        }
    }
}
