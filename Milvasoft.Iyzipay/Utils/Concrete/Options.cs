using Microsoft.Extensions.DependencyInjection;
using Milvasoft.Iyzipay.Utils.Abstract;

namespace Milvasoft.Iyzipay.Utils.Concrete
{
    public class Options : IOptions
    {
        public string ApiKey { get; set; }

        public string SecretKey { get; set; }

        public string BaseUrl { get; set; }

        public ServiceLifetime RestHttpClientLifeTime { get; set; }

        public ServiceLifetime RestHttpClientV2LifeTime { get; set; }
    }
}
