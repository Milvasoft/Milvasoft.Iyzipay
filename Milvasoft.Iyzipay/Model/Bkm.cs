using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class Bkm : PaymentResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public string Token { get; set; }
        public string CallbackUrl { get; set; }


        public Bkm(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<Bkm> RetrieveAsync(RetrieveBkmRequest request)
        {
            return await _restHttpClient.PostAsync<Bkm>("/payment/bkm/auth/detail", request).ConfigureAwait(false);
        }
    }
}
