using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class ThreedsPayment : PaymentResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public ThreedsPayment(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }


        public async Task<ThreedsPayment> CreateAsync(CreateThreedsPaymentRequest request)
        {
            return await _restHttpClient.PostAsync<ThreedsPayment>("/payment/3dsecure/auth", request).ConfigureAwait(false);
        }

        public async Task<ThreedsPayment> RetrieveAsync(RetrievePaymentRequest request)
        {
            return await _restHttpClient.PostAsync<ThreedsPayment>("/payment/detail", request).ConfigureAwait(false);
        }
    }
}
