using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System.Diagnostics;

namespace Milvasoft.Iyzipay.Samples
{
    public class Sample
    {
        protected IOptions Options { get; private set; }

        protected IRestHttpClient RestHttpClient { get; private set; }
        protected IRestHttpClientV2 RestHttpClientV2 { get; private set; }

        [SetUp]
        public void Initialize()
        {
            Options = new Options
            {
                ApiKey = "your api key",
                SecretKey = "your secret key",
                BaseUrl = "https://sandbox-api.iyzipay.com"
            };

            RestHttpClient = new RestHttpClient(new HttpClient(), Options);
            RestHttpClientV2 = new RestHttpClientV2(new HttpClient(), Options);
        }

        protected void PrintResponse<T>(T resource)
        {
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
