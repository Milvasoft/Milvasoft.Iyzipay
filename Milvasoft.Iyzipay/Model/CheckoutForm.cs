using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class CheckoutForm : PaymentResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public string Token { get; set; }
        public string CallbackUrl { get; set; }

        public CheckoutForm(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<CheckoutForm> RetrieveAsync(RetrieveCheckoutFormRequest request)
        {
            return await _restHttpClient.PostAsync<CheckoutForm>("/payment/iyzipos/checkoutform/auth/ecom/detail", request).ConfigureAwait(false);
        }
    }
}
