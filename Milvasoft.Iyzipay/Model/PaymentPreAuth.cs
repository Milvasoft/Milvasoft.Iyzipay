using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class PaymentPreAuth : PaymentResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public PaymentPreAuth(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<PaymentPreAuth> CreateAsync(CreatePaymentRequest request)
        {
            return await _restHttpClient.PostAsync<PaymentPreAuth>("/payment/preauth", request);
        }

        public async Task<PaymentPreAuth> RetrieveAsync(RetrievePaymentRequest request)
        {
            return await _restHttpClient.PostAsync<PaymentPreAuth>("/payment/detail", request);
        }
    }
}
