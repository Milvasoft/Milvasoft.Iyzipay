using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class PayWithIyzicoInitialize : PayWithIyzicoInitializeResource
    {

        private readonly IRestHttpClient _restHttpClient;

        public PayWithIyzicoInitialize(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<PayWithIyzicoInitialize> CreateAsync(CreatePayWithIyzicoInitializeRequest request)
        {
            return await _restHttpClient.PostAsync<PayWithIyzicoInitialize>("/payment/pay-with-iyzico/initialize", request).ConfigureAwait(false);
        }
    }
}
