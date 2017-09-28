using IA.Weather.Services.Contract;

namespace IA.Weather.Services
{
    public class EchoService : IEchoService
    {
        public string Echo(string value)
        {
            return value;
        }
    }
}
