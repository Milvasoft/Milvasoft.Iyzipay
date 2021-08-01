using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class CrossBookingToSubMerchant : IyzipayResource
    {

        private readonly IRestHttpClient _restHttpClient;

        public CrossBookingToSubMerchant(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<CrossBookingToSubMerchant> CreateAsync(CreateCrossBookingRequest request)
        {
            return await _restHttpClient.PostAsync<CrossBookingToSubMerchant>("/crossbooking/send", request).ConfigureAwait(false);
        }
    }
}
