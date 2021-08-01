using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class ThreedsInitializePreAuth : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        [JsonProperty(PropertyName = "threeDSHtmlContent")]
        public string HtmlContent { get; set; }


        public ThreedsInitializePreAuth(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }


        public async Task<ThreedsInitializePreAuth> CreateAsync(CreatePaymentRequest request)
        {
            ThreedsInitializePreAuth response = await _restHttpClient.PostAsync<ThreedsInitializePreAuth>("/payment/3dsecure/initialize/preauth", request).ConfigureAwait(false);

            if (response != null)
            {
                response.HtmlContent = DigestHelper.DecodeString(response.HtmlContent);
            }
            return response;
        }
    }
}
