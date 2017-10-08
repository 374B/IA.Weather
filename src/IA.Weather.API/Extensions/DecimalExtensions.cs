using System;

namespace IA.Weather.API.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToTemperatureString(this decimal self, char unit)
        {
            return $"{Math.Round(self):F}°{unit}";
        }
    }
}
