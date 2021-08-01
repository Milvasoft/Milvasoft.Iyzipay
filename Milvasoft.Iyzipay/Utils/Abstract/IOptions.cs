using Microsoft.Extensions.DependencyInjection;

namespace Milvasoft.Iyzipay.Utils.Abstract
{
    public interface IOptions
    {
        public string ApiKey { get; set; }

        public string SecretKey { get; set; }

        public string BaseUrl { get; set; }

        public ServiceLifetime RestHttpClientLifeTime { get; set; }

        public ServiceLifetime RestHttpClientV2LifeTime { get; set; }
    }
}
