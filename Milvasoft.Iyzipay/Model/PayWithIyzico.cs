using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class PayWithIyzico : PaymentResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public string Token { get; set; }
        public string CallbackUrl { get; set; }

        public PayWithIyzico(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<PayWithIyzico> RetrieveAsync(RetrievePayWithIyzicoRequest request)
        {
            return await _restHttpClient.PostAsync<PayWithIyzico>("/payment/iyzipos/checkoutform/auth/ecom/detail", request).ConfigureAwait(false);
        }
    }
}
