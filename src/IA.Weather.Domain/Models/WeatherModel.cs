using System;

namespace IA.Weather.Domain.Models
{
    //TODO: Expand this class, remove pointless static ctor
    public class WeatherModel
    {
        public string Weather { get; }
        public bool Errored => Error != null;
        public ErrorModel Error { get; }

        public static WeatherModel New(string weather)
        {
            if (string.IsNullOrWhiteSpace(weather)) throw new ArgumentNullException(nameof(weather));

            return new WeatherModel(weather, null);
        }

        /// <summary>
        /// Creates a new WeatherModel from an exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="errorMessage">Exception error message will be used if not specified</param>
        /// <returns></returns>
        public static WeatherModel FromException(Exception ex, string errorMessage = null)
        {
            var errorModel = ErrorModel.FromException(ex);
            return new WeatherModel(null, errorModel);
        }

        private WeatherModel(string weather, ErrorModel error)
        {
            Weather = weather;
            Error = error;
        }
    }
}
