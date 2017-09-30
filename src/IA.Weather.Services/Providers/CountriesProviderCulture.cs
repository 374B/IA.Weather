using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace IA.Weather.Services.Providers
{
    //TODO: Unit test
    //TODO: Move this
    public interface ICountriesProvider
    {
        Task<List<string>> GetCountries();
    }
    /// <summary>
    /// Get a list of countries from the .NET culture libs
    /// </summary>
    public class CountriesProviderCulture : ICountriesProvider
    {
        public async Task<List<string>> GetCountries()
        {
            var countries = new List<string>();
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            foreach (var cultureInfo in cultures)
            {
                //Skip neutral cultures
                if (cultureInfo.IsNeutralCulture) continue;

                try
                {
                    var regionInfo = new RegionInfo(cultureInfo.LCID);
                    countries.Add(regionInfo.EnglishName);
                }
                catch (ArgumentException argEx)
                {
                    //Some cultures don't have regions, ignore this specific ex and cotinue
                    if (argEx.Message.StartsWith("There is no region associated with the Invariant Culture"))
                        continue;

                    throw;
                }
            }

            countries = countries.Distinct().ToList();

            return countries;

        }
    }
}
