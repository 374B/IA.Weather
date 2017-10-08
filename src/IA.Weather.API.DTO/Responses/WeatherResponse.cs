using System.Collections.Generic;

namespace IA.Weather.API.DTO.Responses
{
    public class WeatherResponse
    {
        public string MainDesc { get; set; }
        public string SecondaryDesc { get; set; }
        public string CurrentTemp { get; set; }

        public Dictionary<string, object> Props { get; set; }

        public WeatherResponse()
        {
            Props = new Dictionary<string, object>();
        }

    }
}
