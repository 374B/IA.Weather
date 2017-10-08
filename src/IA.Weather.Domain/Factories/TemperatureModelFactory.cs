using IA.Weather.Domain.Calculators;
using IA.Weather.Domain.Models;

namespace IA.Weather.Domain.Factories
{
    public interface ITemperatureModelFactory
    {
        TemperatureModel FromCelsius(decimal degreesCelsius);
        TemperatureModel FromFahrenheit(decimal degreesFahrenheit);
        TemperatureModel FromKelvin(decimal degreesFahrenheit);
    }

    public class TemperatureModelFactory : ITemperatureModelFactory
    {
        private readonly ITemperatureCalculator _calc;

        public TemperatureModelFactory(ITemperatureCalculator calc)
        {
            _calc = calc;
        }

        public TemperatureModel FromCelsius(decimal degreesCelsius)
        {
            var fh = _calc.CelsiusToFahrenheit(degreesCelsius);
            return new TemperatureModel(degreesCelsius, fh);

        }

        public TemperatureModel FromFahrenheit(decimal degreesFahrenheit)
        {
            var c = _calc.FahrenheitToCelsius(degreesFahrenheit);
            return new TemperatureModel(c, degreesFahrenheit);
        }

        public TemperatureModel FromKelvin(decimal degreesKelvin)
        {
            var c = _calc.KelvinToCelsius(degreesKelvin);
            var f = _calc.KelvinToFahrenheit(degreesKelvin);

            return new TemperatureModel(c, f);

        }
    }
}
