using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IA.Weather.API.Controllers;
using IA.Weather.API.DTOs.Responses;
using IA.Weather.API.UnitTests.Extensions;
using IA.Weather.Services.Contract.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IA.Weather.API.UnitTests.Tests
{
    [TestClass]
    public class CountriesControllerTests
    {
        [TestMethod]
        public async Task GetAllCountries_Should_Return_A_List_Of_Countries()
        {
            //Arrange

            var data = new List<string> { "Australia", "England" };

            var mock = new Mock<ICountriesService>();
            mock.Setup(x => x.GetAllCountries()).ReturnsAsync(data);

            var sut = new CountriesController(mock.Object)
                .SetupForTesting();

            //Act

            var result = await sut.GetAllCountries();

            //Assert

            result.AssertHttp()
                .StatusCodeOk()
                .ContentOfType(out CountriesResponse response);

            var equal = data.SequenceEqual(response.Countries.Select(x => x.Name));

            Assert.IsTrue(equal, "Response did not contain the expected countries");

        }

        [TestMethod]
        public async Task GetCitiesByCountry_Should_Return_A_BadRequest_If_Country_Is_Null()
        {
            //Arrange

            var mock = new Mock<ICountriesService>();

            var sut = new CountriesController(mock.Object)
                .SetupForTesting();

            //Act

            var result = await sut.GetCitiesByCountry(null);

            //Assert

            result.AssertHttp()
                .StatusCode(HttpStatusCode.BadRequest);

        }

        [TestMethod]
        public async Task GetCitiesByCountry_Should_Return_A_BadRequest_If_Country_Is_Empty()
        {
            //Arrange

            var mock = new Mock<ICountriesService>();

            var sut = new CountriesController(mock.Object)
                .SetupForTesting();

            //Act

            var result = await sut.GetCitiesByCountry(string.Empty);

            //Assert

            result.AssertHttp()
                .StatusCode(HttpStatusCode.BadRequest);

        }

    }
}
