using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class BasicThreedsInitialize : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        [JsonProperty(PropertyName = "threeDSHtmlContent")]
        public string HtmlContent { get; set; }

        public BasicThreedsInitialize(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<BasicThreedsInitialize> CreateAsync(CreateBasicPaymentRequest request)
        {
            BasicThreedsInitialize response = await _restHttpClient.PostAsync<BasicThreedsInitialize>("/payment/3dsecure/initialize/basic", request).ConfigureAwait(false);

            if (response != null)
            {
                response.HtmlContent = DigestHelper.DecodeString(response.HtmlContent);
            }
            return response;
        }
    }
}
