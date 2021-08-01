using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class BasicPayment : BasicPaymentResource
    {

        private readonly IRestHttpClient _restHttpClient;

        public BasicPayment(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<BasicPayment> CreateAsync(CreateBasicPaymentRequest request)
        {
            return await _restHttpClient.PostAsync<BasicPayment>("/payment/auth/basic", request).ConfigureAwait(false);
        }
    }
}
