using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IA.Weather.Infrastructure.Providers.Interfaces;
using Newtonsoft.Json;

namespace IA.Weather.Infrastructure.Providers.Implementations
{
    public class CountriesProviderRestCountriesEu : ICountriesProvider
    {
        private readonly string _url;

        public CountriesProviderRestCountriesEu(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));

            _url = url;
        }

        public async Task<List<string>> GetCountries()
        {
            using (var httpClient = new HttpClient())
            {
                var res = await httpClient.GetAsync(_url);
                res.EnsureSuccessStatusCode();

                var str = await res.Content.ReadAsStringAsync();
                dynamic dyn = JsonConvert.DeserializeObject(str);

                var results = new List<string>();

                foreach (var item in dyn)
                {
                    var name = (string)item.name;
                    if (string.IsNullOrWhiteSpace(name)) continue;

                    results.Add(name);
                }

                results = results.Distinct()
                    .ToList();

                return results;

            }
        }
    }
}
