using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class BasicPaymentPostAuth : BasicPaymentResource
    {

        private readonly IRestHttpClient _restHttpClient;

        public BasicPaymentPostAuth(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<BasicPaymentPostAuth> CreateAsync(CreatePaymentPostAuthRequest request)
        {
            return await _restHttpClient.PostAsync<BasicPaymentPostAuth>("/payment/postauth/basic", request).ConfigureAwait(false);
        }
    }
}
