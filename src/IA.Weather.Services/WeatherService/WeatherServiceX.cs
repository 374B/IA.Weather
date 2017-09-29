namespace IA.Weather.Services.WeatherService
{
    public class WeatherServiceX : WeatherServiceBase
    {
        public override string Identifier => "X";
        public override string Name => "Webservicex.net";
        public override string Description => "Weather Served from Webservicex.net";

        public WeatherServiceX(IWeatherProviderX provider) : base(provider)
        {
        }
    }

    public interface IWeatherProviderX : IWeatherProvider { }

}
