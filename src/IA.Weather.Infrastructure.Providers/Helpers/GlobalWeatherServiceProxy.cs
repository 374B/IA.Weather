using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IA.Weather.Infrastructure.Providers.Helpers
{
    public class GlobalWeatherServiceProxy
    {
        private readonly string _endpointAddress;

        public GlobalWeatherServiceProxy(string endpointAddress)
        {
            _endpointAddress = endpointAddress;
        }

        public async Task<T> Invoke<T>(Func<GlobalWeatherServiceReference.GlobalWeatherSoapClient, Task<T>> method)
        {
            //TODO: Basic ex handling
            var addr = new EndpointAddress(_endpointAddress);
            var binding = new BasicHttpBinding();

            using (var client = new GlobalWeatherServiceReference.GlobalWeatherSoapClient(binding, addr))
            {
                var result = await method(client);
                return result;
            }
        }
    }
}
