using System.Collections.Generic;

namespace IA.Weather.Web.Models
{
    public class AppViewModel
    {
        public List<CountryViewModel> Countries { get; set; }
        public List<WeatherServiceViewModel> WeatherServices { get; set; }
        public List<string> Errors { get; } = new List<string>();
    }

    public class CountryViewModel
    {
        public string Name { get; set; }
        public string CitiesLink { get; set; }
    }

    public class WeatherServiceViewModel
    {
        public string Name { get; set; }
        public string WeatherLink { get; set; }
    }
}