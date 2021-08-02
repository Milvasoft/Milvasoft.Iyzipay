using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System.Diagnostics;
using System.Net.Http;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class BaseTest
    {
        protected Options _options;

        protected IRestHttpClient RestHttpClient { get; private set; }
        protected IRestHttpClientV2 RestHttpClientV2 { get; private set; }

        [SetUp]
        public void Initialize()
        {
            _options = new Options
            {
                ApiKey = "sandbox-afXhZPW0MQlE4dCUUlHcEopnMBgXnAZI",
                SecretKey = "sandbox-wbwpzKIiplZxI3hh5ALI4FJyAcZKL6kq",
                BaseUrl = "https://sandbox-api.iyzipay.com"
            };

            RestHttpClient = new RestHttpClient(new HttpClient(), _options);
            RestHttpClientV2 = new RestHttpClientV2(new HttpClient(), _options);
        }

        protected static void PrintResponse<T>(T resource)
        {
#if RELEASE
            return;
#endif

#if NETCORE1 || NETCORE2
            TraceListener consoleListener = new TextWriterTraceListener(System.Console.Out);
#else
            TraceListener consoleListener = new ConsoleTraceListener();
#endif

            Trace.Listeners.Add(consoleListener);
            Trace.WriteLine(JsonConvert.SerializeObject(resource, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
        }
    }
}
