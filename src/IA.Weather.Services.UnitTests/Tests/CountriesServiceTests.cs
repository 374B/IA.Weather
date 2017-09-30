using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IA.Weather.Infrastructure.Providers.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IA.Weather.Services.Test.Tests
{
    [TestClass]
    public class CountriesServiceTests
    {
        [TestMethod]
        public async Task GetAllCountries_Should_Cache_Provider_Result()
        {
            //Arrange

            var mock = new Mock<ICountriesProvider>();
            mock.Setup(x => x.GetCountries()).ReturnsAsync(new List<string> {"A", "B", "C"});
            var sut = new CountriesService.CountriesService(mock.Object);

            //Act

            var result = await sut.GetAllCountries();
            var result2 = await sut.GetAllCountries();
            var result3 = await sut.GetAllCountries();

            //Assert

            mock.Verify(x => x.GetCountries(), Times.Once);

            Assert.IsTrue(result.SequenceEqual(result2));
            Assert.IsTrue(result2.SequenceEqual(result3));
        }
    }
}
