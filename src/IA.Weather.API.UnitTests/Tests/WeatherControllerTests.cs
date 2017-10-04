using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IA.Weather.API.Controllers;
using IA.Weather.API.Helpers;
using IA.Weather.API.UnitTests.Extensions;
using IA.Weather.Services.Contract.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IA.Weather.API.UnitTests.Tests
{
    [TestClass]
    public class WeatherControllerTests
    {
        [TestMethod]
        public async Task WeatherController_AvailableServices_Should_Return_1_Result_For_Each_Service()
        {
            //Arrange

            var routeProviderMock = new Mock<IRouteProvider>();

            var weatherServiceMocks = new List<Mock<IWeatherService>>
            {
                new Mock<IWeatherService>(),
                new Mock<IWeatherService>(),
                new Mock<IWeatherService>(),
            };

            weatherServiceMocks.ForEach(m =>
                m.SetupGet(x => x.Identifier)
                    .Returns(weatherServiceMocks.IndexOf(m).ToString()));

            var sut = new WeatherController(routeProviderMock.Object, weatherServiceMocks.Select(m => m.Object))
                .SetupForTesting();

            //Act

            var result = sut.AvailableServices();

            //Assert

            result.AssertHttp()
                .StatusCodeOk()
                .Content(out _);
        }
    }
}
