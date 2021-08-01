using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class BkmInitialize : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public string HtmlContent { get; set; }
        public string Token { get; set; }

        public BkmInitialize(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<BkmInitialize> CreateAsync(CreateBkmInitializeRequest request)
        {
            BkmInitialize response = await _restHttpClient.PostAsync<BkmInitialize>("/payment/bkm/initialize", request);

            if (response != null)
            {
                response.HtmlContent = DigestHelper.DecodeString(response.HtmlContent);
            }

            return response;
        }
    }
}
