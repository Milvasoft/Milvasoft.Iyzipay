using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class BasicPaymentPreAuth : BasicPaymentResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public BasicPaymentPreAuth(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<BasicPaymentPreAuth> CreateAsync(CreateBasicPaymentRequest request)
        {
            return await _restHttpClient.PostAsync<BasicPaymentPreAuth>("/payment/preauth/basic", request);
        }
    }
}
