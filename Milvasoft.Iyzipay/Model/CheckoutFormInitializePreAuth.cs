using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class CheckoutFormInitializePreAuth : CheckoutFormInitializeResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public CheckoutFormInitializePreAuth(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<CheckoutFormInitializePreAuth> CreateAsync(CreateCheckoutFormInitializeRequest request)
        {
            return await _restHttpClient.PostAsync<CheckoutFormInitializePreAuth>("/payment/iyzipos/checkoutform/initialize/preauth/ecom", request).ConfigureAwait(false);
        }
    }
}
