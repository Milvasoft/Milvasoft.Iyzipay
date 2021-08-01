using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class CheckoutFormInitialize : CheckoutFormInitializeResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public CheckoutFormInitialize(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<CheckoutFormInitialize> CreateAsync(CreateCheckoutFormInitializeRequest request)
        {
            return await _restHttpClient.PostAsync<CheckoutFormInitialize>("/payment/iyzipos/checkoutform/initialize/auth/ecom", request).ConfigureAwait(false);
        }
    }
}
