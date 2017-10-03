using System.Collections.Generic;

namespace IA.Weather.API.DTO.Responses
{
    public class WeatherServicesResponse
    {
        public List<WeatherServiceResponse> Services { get; set; }

        public WeatherServicesResponseMeta Meta { get; set; }

    }

    public class WeatherServicesResponseMeta
    {
        public int Count { get; set; }
    }

}
