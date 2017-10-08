using System;

namespace IA.Weather.Domain.Models
{
    public class WeatherModel
    {
        public string Description1 { get; }
        public string Description2 { get; }
        public TemperatureModel TempMin { get; }
        public TemperatureModel TempMax { get; }
        public TemperatureModel TempCurrent { get; }
        public decimal? Pressure { get; }
        public decimal? Humidity { get; }
        public decimal? WindSpeed { get; }
        public decimal? WindDeg { get; }

        public bool Errored => Error != null;
        public ErrorModel Error { get; }

        /// <summary>
        /// Creates a new WeatherModel from an exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="errorMessage">Exception error message will be used if not specified</param>
        /// <returns></returns>
        public static WeatherModel FromException(Exception ex, string errorMessage = null)
        {
            if (ex == null) throw new ArgumentNullException(nameof(ex));

            var errorModel = ErrorModel.FromException(ex);
            return new WeatherModel(errorModel);
        }

        public WeatherModel(string description1, string description2) : this(
            description1, description2,
            null, null, null,
            null, null,
            null, null)
        {

        }
        
        public WeatherModel(
            string description1, string description2,
            TemperatureModel tempMin, TemperatureModel tempMax, TemperatureModel tempCurrent,
            decimal? pressure, decimal? humidity,
            decimal? windSpeed, decimal? windDeg)
        {
            Description1 = description1;
            Description2 = description2;
            TempMin = tempMin;
            TempMax = tempMax;
            TempCurrent = tempCurrent;
            Pressure = pressure;
            Humidity = humidity;
            WindSpeed = windSpeed;
            WindDeg = windDeg;
        }

        public WeatherModel(ErrorModel error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));

            Error = error;
        }
    }
}
