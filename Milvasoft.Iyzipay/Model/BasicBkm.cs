using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class BasicBkm : BasicPaymentResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public string Token { get; set; }
        public string CallbackUrl { get; set; }
        public string PaymentStatus { get; set; }

        public BasicBkm(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<BasicBkm> RetrieveAsync(RetrieveBkmRequest request)
        {
            return await _restHttpClient.PostAsync<BasicBkm>("/payment/bkm/auth/detail/basic", request).ConfigureAwait(false);
        }
    }
}
