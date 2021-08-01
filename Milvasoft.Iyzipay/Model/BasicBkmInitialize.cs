using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class BasicBkmInitialize : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public string HtmlContent { get; set; }
        public string Token { get; set; }

        public BasicBkmInitialize(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<BasicBkmInitialize> CreateAsync(CreateBasicBkmInitializeRequest request)
        {
            BasicBkmInitialize response = await _restHttpClient.PostAsync<BasicBkmInitialize>("/payment/bkm/initialize/basic", request).ConfigureAwait(false);

            if (response != null)
            {
                response.HtmlContent = DigestHelper.DecodeString(response.HtmlContent);
            }
            return response;
        }
    }
}
