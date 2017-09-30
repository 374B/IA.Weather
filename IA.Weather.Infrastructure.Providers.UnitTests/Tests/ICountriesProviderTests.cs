using System;
using System.Linq;
using System.Threading.Tasks;
using IA.Weather.Infrastructure.Providers.Implementations;
using IA.Weather.Infrastructure.Providers.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IA.Weather.Infrastructure.Providers.UnitTests.Tests
{
    //We can use this pattern to run the same unit tests against different implementations of an interface
    //.. If we were using NUnit we get this for free
    //.. One limitation of this approach is debugging individual tests
    public abstract class CountriesProviderTests
    {
        [TestMethod]
        public async Task GetCountries_Should_Return_A_List_Of_Countries()
        {
            //Arrange

            var sut = GetInstance();

            //Act

            var results = await sut.GetCountries();

            //Assert

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);

            Console.WriteLine(string.Join("\n", results));

        }

        [TestMethod]
        public async Task GetCountries_Should_Not_Contain_Duplicates()
        {
            //Arrange

            var sut = GetInstance();

            //Act

            var results = await sut.GetCountries();

            //Assert

            var grouped = results.GroupBy(x => x);

            var duplicates = grouped.Where(g => g.Count() > 1)
                .Select(g => $"{g.Key} : {g.Count()}")
                .ToList();

            if (duplicates.Any())
                Assert.Fail($"Duplicates found:\n{string.Join("\n", duplicates)}");

        }

        [TestMethod]
        public async Task GetCountries_Should_Not_Any_Empty_Items()
        {
            //Arrange

            var sut = GetInstance();

            //Act

            var results = await sut.GetCountries();

            //Assert

            var emtpy = results.Where(string.IsNullOrWhiteSpace);

            if (emtpy.Any())
                Assert.Fail("Empty items found in result set");

        }

        protected abstract ICountriesProvider GetInstance();
    }

    [TestClass]
    public class CountriesProiderFromCultureTests : CountriesProviderTests
    {
        protected override ICountriesProvider GetInstance()
        {
            return new CountriesProviderFromCulture();
        }
    }
    
    [TestClass]
    public class CountriesProiderFromRegistryTests : CountriesProviderTests
    {
        protected override ICountriesProvider GetInstance()
        {
            //Hardcoding this here isn't great, it creates a maintenance problem
            //... Not to mention we aren't actually testing anything if this url returns differnet data from the one used in an actual environment
            //... You could also debate about whether or not a unit test should make a network call
            //... It probably shouldn't, but the point of this was just to demonstrate a way to test multiple implemenations of an interface
            var url = "https://restcountries.eu/rest/v2/all?fields=name";

            return new CountriesProviderRestCountriesEu(url);
        }
    }
}
