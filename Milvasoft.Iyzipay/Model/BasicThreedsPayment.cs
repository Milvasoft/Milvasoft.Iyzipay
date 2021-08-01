using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class BasicThreedsPayment : BasicPaymentResource
    {

        private readonly IRestHttpClient _restHttpClient;

        public BasicThreedsPayment(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<BasicThreedsPayment> CreateAsync(CreateThreedsPaymentRequest request)
        {
            return await _restHttpClient.PostAsync<BasicThreedsPayment>("/payment/3dsecure/auth/basic", request).ConfigureAwait(false);
        }
    }
}
