namespace IA.Weather.Domain.Calculators
{
    //TODO: Test
    public interface ITemperatureCalculator
    {
        decimal FahrenheitToCelsius(decimal degreesFahrenehit);
        decimal CelsiusToFahrenheit(decimal degreesCelsius);
        decimal KelvinToFahrenheit(decimal degreesKelvin);
        decimal KelvinToCelsius(decimal degreesKelvin);

        //TODO: Missing methods
        //F to K
        //C to K
    }

    public class TemperatureCalculator : ITemperatureCalculator
    {
        public decimal FahrenheitToCelsius(decimal degreesFahrenehit)
        {
            return (degreesFahrenehit - 32) * 5m / 9m;
        }

        public decimal CelsiusToFahrenheit(decimal degreesCelsius)
        {
            return degreesCelsius * (9m / 5m) + 32;
        }

        public decimal KelvinToFahrenheit(decimal degreesKelvin)
        {
            return degreesKelvin * (9m / 5m) - 459.67m;
        }

        public decimal KelvinToCelsius(decimal degreesKelvin)
        {
            return degreesKelvin - 273.15m;
        }
    }
}
