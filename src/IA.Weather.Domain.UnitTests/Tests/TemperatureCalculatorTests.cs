using IA.Weather.Domain.Calculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IA.Weather.Domain.UnitTests.Tests
{
    [TestClass]
    public class TemperatureCalculatorTests
    {
        [TestMethod]
        public void Celsius_To_Fahrenheit_Should_Be_Calculated_Correctly()
        {
            //Arrange

            var degC = 30m;
            var sut = new TemperatureCalculator();

            //Act

            var degF = sut.CelsiusToFahrenheit(degC);

            //Assert

            //30 degrees celsius should equal 86 fahrenheit
            Assert.AreEqual(86, degF);

        }

        [TestMethod]
        public void Fahrenheit_To_Celsius_Should_Be_Calculated_Correctly()
        {
            //Arrange

            var degF = 86m;
            var sut = new TemperatureCalculator();

            //Act

            var degC = sut.FahrenheitToCelsius(degF);

            //Assert

            //30 degrees celsius should equal 86 fahrenheit
            Assert.AreEqual(30, degC);
        }

        [TestMethod]
        public void Kelvin_To_Celsius_Should_Be_Calculated_Correctly()
        {
            //Arrange

            var degK = 300m;
            var sut = new TemperatureCalculator();

            //Act

            var degC = sut.KelvinToCelsius(degK);

            //Assert

            //30 degrees celsius should equal 86 fahrenheit
            Assert.AreEqual(26.85m, degC);
        }

        [TestMethod]
        public void Kelvin_To_Fahrenheit_Should_Be_Calculated_Correctly()
        {
            //Arrange

            var degK = 300m;
            var sut = new TemperatureCalculator();

            //Act

            var degC = sut.KelvinToFahrenheit(degK);

            //Assert

            //30 degrees celsius should equal 86 fahrenheit
            Assert.AreEqual(80.33m, degC);
        }


    }
}
