using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class BasicThreedsInitializePreAuth : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        [JsonProperty(PropertyName = "threeDSHtmlContent")]
        public string HtmlContent { get; set; }

        public BasicThreedsInitializePreAuth(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<BasicThreedsInitializePreAuth> CreateAsync(CreateBasicPaymentRequest request)
        {
            BasicThreedsInitializePreAuth response = await _restHttpClient.PostAsync<BasicThreedsInitializePreAuth>("/payment/3dsecure/initialize/preauth/basic", request).ConfigureAwait(false);

            if (response != null)
            {
                response.HtmlContent = DigestHelper.DecodeString(response.HtmlContent);
            }
            return response;
        }
    }
}
