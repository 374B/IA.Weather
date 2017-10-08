using System.Globalization;

namespace IA.Weather.API.Extensions
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string self)
        {
            if (string.IsNullOrWhiteSpace(self)) return self;

            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(self);
        }

        public static string UpperFirst(this string self)
        {
            if (string.IsNullOrWhiteSpace(self)) return self;

            return char.ToUpper(self[0]) + self.Substring(1, self.Length - 1);

        }

    }
}
