using System;
using System.Collections.Generic;
using IA.Weather.API.DTO.Responses;
using IA.Weather.API.Extensions;
using IA.Weather.Domain.Models;

namespace IA.Weather.API.Mappers
{
    public static class WeatherModelMapper
    {
        public static WeatherResponse ToWeatherResponse(WeatherModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (model.Errored) throw new InvalidOperationException("An errored model can not be mapped to a response");

            var res = new WeatherResponse();

            res.MainDesc = model.Description1.UpperFirst();
            res.SecondaryDesc = model.Description2.UpperFirst();

            res.CurrentTemp = model.TempCurrent != null ?
                model.TempCurrent.DegreesCelsius.ToTemperatureString('C')
                : "Unknown";

            AddTemperatureModel(res.Props, "Current Temperature", model.TempCurrent);
            AddTemperatureModel(res.Props, "Min. Temperature", model.TempMin);
            AddTemperatureModel(res.Props, "Max. Temperature", model.TempMax);

            res.Props.Add("Humidity", model.Humidity);
            res.Props.Add("Pressure", model.Pressure);
            res.Props.Add("Wind Speed", model.WindSpeed);

            return res;

        }

        private static void AddTemperatureModel(Dictionary<string, object> propsDict, string name, TemperatureModel model)
        {
            if (model == null)
            {
                propsDict.Add(name, "Unknown");
                return;
            }

            var str = $"{model.DegreesCelsius.ToTemperatureString('C')} / {model.DegreesFahrenheit.ToTemperatureString('F')}";
            propsDict.Add(name, str);

        }

    }
}
