using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml.Linq;
using IA.Weather.Infrastructure.Providers.Interfaces;

namespace IA.Weather.Infrastructure.Providers.Implementations
{
    public class CitiesProviderX : ICitiesProvider
    {
        private readonly string _endpointAddress;

        public CitiesProviderX(string endpointAddress)
        {
            _endpointAddress = endpointAddress;
        }

        public async Task<List<string>> CitiesForCountry(string country)
        {
            //TODO: Basic ex handling
            var addr = new EndpointAddress(_endpointAddress);
            var binding = new BasicHttpBinding();

            var results = new List<string>();

            using (var client = new GlobalWeatherServiceReference.GlobalWeatherSoapClient(binding, addr))
            {
                var data = await client.GetCitiesByCountryAsync(country);

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
}
