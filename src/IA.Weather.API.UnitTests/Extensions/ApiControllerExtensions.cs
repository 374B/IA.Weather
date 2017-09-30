using System.Net.Http;
using System.Web.Http;

namespace IA.Weather.API.UnitTests.Extensions
{
    public static class ApiControllerExtensions
    {
        /// <summary>
        /// Sets an empty request/configuration
        /// </summary>
        /// <param name="self"></param>
        /// <param name="config">If null, new instance will be used</param>
        /// <param name="req">If null, new instance will be used</param>
        /// <returns></returns>
        public static T SetupForTesting<T>(this T self, HttpConfiguration config = null, HttpRequestMessage req = null) where T : ApiController
        {
            self.Configuration = config ?? new HttpConfiguration();
            self.Request = req ?? new HttpRequestMessage();
            return self;
        }
    }
}
