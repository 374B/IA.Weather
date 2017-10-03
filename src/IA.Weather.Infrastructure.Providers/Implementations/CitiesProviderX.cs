using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using IA.Weather.Infrastructure.Providers.Interfaces;

namespace IA.Weather.Infrastructure.Providers.Implementations
{
    public class CitiesProviderX : ICitiesProvider
    {
        public async Task<List<string>> CitiesForCountry(string country)
        {
            var addr = new EndpointAddress("http://www.webservicex.net/globalweather.asmx");
            var binding = new BasicHttpBinding();

            var results = new List<string>();

            using (var client = new GlobalWeatherServiceReference.GlobalWeatherSoapClient(binding, addr))
            {
                var data = await client.GetCitiesByCountryAsync(country);

                using (var tr = new StringReader(data))
                {
                    var xdoc = XDocument.Load(tr);
                    var cityElements = xdoc.XPathSelectElements("/newdataset/table/country").Elements("City").ToList();

                    if (cityElements.Any())
                        results.AddRange(cityElements.Select(e => e.Value.Trim()));

                    return results;

                }
            }
        }
    }
}
