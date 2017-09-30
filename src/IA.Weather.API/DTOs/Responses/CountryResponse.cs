using System.Collections.Generic;

namespace IA.Weather.API.DTOs.Responses
{
    public class CountryResponse
    {
        public string Name { get; set; }
        public List<LinkResponse> Links { get; set; }
    }
}
