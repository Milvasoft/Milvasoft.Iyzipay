using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class ThreedsInitialize : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        [JsonProperty(PropertyName = "threeDSHtmlContent")]
        public string HtmlContent { get; set; }



        public ThreedsInitialize(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }


        public async Task<ThreedsInitialize> CreateAsync(CreatePaymentRequest request)
        {
            ThreedsInitialize response = await _restHttpClient.PostAsync<ThreedsInitialize>("/payment/3dsecure/initialize", request).ConfigureAwait(false);

            if (response != null)
            {
                response.HtmlContent = DigestHelper.DecodeString(response.HtmlContent);
            }
            return response;
        }
    }
}
