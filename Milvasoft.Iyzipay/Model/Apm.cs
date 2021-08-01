using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class Apm : ApmResource
    {

        private readonly IRestHttpClient _restHttpClient;

        public Apm(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<Apm> CreateAsync(CreateApmInitializeRequest request)
        {
            return await _restHttpClient.PostAsync<Apm>("/payment/apm/initialize", request);
        }

        public async Task<Apm> RetrieveAsync(RetrieveApmRequest request)
        {
            return await _restHttpClient.PostAsync<Apm>("/payment/apm/retrieve", request).ConfigureAwait(false);
        }
    }
}
