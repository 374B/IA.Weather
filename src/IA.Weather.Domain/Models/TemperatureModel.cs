namespace IA.Weather.Domain.Models
{
    public class TemperatureModel
    {
        public decimal DegreesCelsius { get; }
        public decimal DegreesFahrenheit { get; }

        internal TemperatureModel(decimal degreesCelsius, decimal degreesFahrenheit)
        {
            DegreesCelsius = degreesCelsius;
            DegreesFahrenheit = degreesFahrenheit;
        }
    }
}
