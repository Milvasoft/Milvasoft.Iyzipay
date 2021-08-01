using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class PaymentPostAuth : PaymentResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public PaymentPostAuth(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<PaymentPostAuth> CreateAsync(CreatePaymentPostAuthRequest request)
        {
            return await _restHttpClient.PostAsync<PaymentPostAuth>("/payment/postauth", request).ConfigureAwait(false);
        }
    }
}
