using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class ApiTest : IyzipayResource
    {

        private readonly IRestHttpClient _restHttpClient;

        public ApiTest(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<IyzipayResource> RetrieveAsync()
        {
            return await _restHttpClient.GetAsync<IyzipayResource>("/payment/test").ConfigureAwait(false);
        }
    }
}
