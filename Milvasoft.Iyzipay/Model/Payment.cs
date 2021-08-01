using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class Payment : PaymentResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public Payment(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }


        public async Task<Payment> CreateAsync(CreatePaymentRequest request)
        {
            return await _restHttpClient.PostAsync<Payment>("/payment/auth", request);
        }

        public async Task<Payment> RetrieveAsync(RetrievePaymentRequest request)
        {
            return await _restHttpClient.PostAsync<Payment>("/payment/detail", request);
        }
    }
}
