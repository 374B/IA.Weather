using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace IA.Weather.API.UnitTests.Extensions
{
    public static class IHttpActionResultExtensions
    {
        public static AssertHttpFluent AssertHttp(this IHttpActionResult self)
        {
            return new AssertHttpFluent(self);
        }

        public class AssertHttpFluent
        {
            private readonly IHttpActionResult _result;
            private readonly HttpResponseMessage _response;

            public AssertHttpFluent(IHttpActionResult result)
            {
                _result = result;
                _response = result.ExecuteAsync(CancellationToken.None).Result;
            }

            public AssertHttpFluent StatusCodeOk()
            {
                return StatusCode(HttpStatusCode.OK);
            }

            public AssertHttpFluent StatusCode(HttpStatusCode expected)
            {
                Assert.AreEqual(expected, _response.StatusCode);
                return this;
            }

            /// <summary>
            /// Check the response has content
            /// </summary>
            /// <returns></returns>
            public AssertHttpFluent Content(out string stringContent)
            {
                Assert.IsNotNull(_response.Content, "Content was null");

                stringContent = _response.Content.ReadAsStringAsync().Result;

                Console.WriteLine(stringContent);

                if (string.IsNullOrWhiteSpace(stringContent) || stringContent.Equals("\"\""))
                    Assert.Fail("Content was empty");

                return this;
            }

            /// <summary>
            /// Check the response has content of type T
            /// Uses default deserializer
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public AssertHttpFluent ContentOfType<T>(out T content)
                where T : class
            {
                return ContentOfType(JsonConvert.DeserializeObject<T>, out content);
            }

            /// <summary>
            /// Check the response has content of type T
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="deserializer"></param>
            /// <param name="content"></param>
            /// <returns></returns>
            public AssertHttpFluent ContentOfType<T>(Func<string, T> deserializer, out T content)
                where T : class
            {
                content = null;

                Content(out string strContent);

                try
                {
                    content = deserializer(strContent);
                    if (content == null) throw new Exception("Null object was returned");

                }
                catch (Exception ex)
                {
                    Assert.Fail($"Could not deserialize content into {typeof(T)}. Exception: {ex}");
                }

                return this;

            }
        }
    }
}
