using System;
using System.Threading.Tasks;
using IA.Weather.Domain.Models;
using IA.Weather.Services.Contract.Interfaces;
using IA.Weather.Services.WeatherService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IA.Weather.Services.Test
{
    [TestClass]
    public class WeatherServiceTest
    {
        [TestMethod]
        public async Task WeatherServiceX_GetByCountry_Should_Call_The_Provider_Once()
        {
            await WeatherService_GetByCountry_Should_Call_The_Provider_Once<IWeatherProviderX>(
                provider => new WeatherServiceX(provider));
        }

        [TestMethod]
        public async Task WeatherServicePhil_GetByCountry_Should_Call_The_Provider_Once()
        {
            await WeatherService_GetByCountry_Should_Call_The_Provider_Once<IWeatherProviderOpenWeatherMap>(
                provider => new WeatherServiceOpenWeatherMap(provider));
        }

        [TestMethod]
        public async Task WeatherServicePhilly_GetByCountry_Should_Call_The_Provider_Once()
        {
            await WeatherService_GetByCountry_Should_Call_The_Provider_Once<IWeatherProviderPhilly>(
                provider => new WeatherServicePhilly(provider));
        }

        /// <summary>
        /// Basic test to ensure that the service is calling the provide once and a result is returned
        /// The provider itself is not tested (it is mocked) as we aren't testing the provider, only the service behavior
        /// </summary>
        /// <typeparam name="TProvider"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        private async Task WeatherService_GetByCountry_Should_Call_The_Provider_Once<TProvider>(Func<TProvider, IWeatherService> factory)
            where TProvider : class, IWeatherProvider
        {
            //Arrange

            var provider = new Mock<TProvider>();
            provider.Setup(x => x.GetWeatherResponse(It.IsAny<WeatherRequest>())).Returns(Task.FromResult(WeatherModel.New("Test")));
            var sut = factory(provider.Object);

            //Act

            var result = await sut.GetByCountry(string.Empty);

            //Assert
            Assert.IsNotNull(result, "A result should be returned");
            provider.Verify(x => x.GetWeatherResponse(It.IsAny<WeatherRequest>()), Times.Once, "The provider method was not called once");

        }

    }
}
