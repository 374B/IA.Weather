namespace IA.Weather.Services.Contract.Interfaces
{
    public interface IWeatherService
    {
        string Identifier { get; }
        string Name { get; }
        string Description { get; }
    }
}
