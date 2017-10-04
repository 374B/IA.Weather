using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using IA.Weather.Web.Models;

namespace IA.Weather.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiClient _apiClient;

        //TODO: Remvoe this, use DI
        public HomeController() : this(new ApiClient()) { }

        public HomeController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ActionResult> Index()
        {
            var vm = new AppViewModel();

            await Task.WhenAll(
                Task.Run(() => PopulateCountries(vm)),
                Task.Run(() => PopulateWeatherServices(vm)));

            return View(vm);

        }

        private async Task PopulateCountries(AppViewModel viewModel)
        {
            viewModel.Countries = new List<CountryViewModel>();

            try
            {
                var res = await _apiClient.GetCountriesList();

                foreach (var country in res.Countries)
                {
                    viewModel.Countries.Add(new CountryViewModel
                    {
                        Name = country.Name,
                        CitiesLink = country.Links
                            .First(l => l.Rel.Equals("cities", StringComparison.OrdinalIgnoreCase)).Href
                    });
                }
            }
            catch (Exception ex)
            {
                //TODO: Log
                viewModel.Errors.Add("Could not get the list of countries");
            }

        }

        private async Task PopulateWeatherServices(AppViewModel viewModel)
        {
            viewModel.WeatherServices = new List<WeatherServiceViewModel>();

            try
            {
                var res = await _apiClient.GetWeatherServicesList();

                foreach (var s in res.Services)
                {
                    viewModel.WeatherServices.Add(new WeatherServiceViewModel
                    {
                        Name = s.Name,
                        WeatherLink = s.Link
                    });
                }
            }
            catch (Exception ex)
            {
                //TODO: Log
                viewModel.Errors.Add("Could not get the list of providers");
            }
        }
    }

}
