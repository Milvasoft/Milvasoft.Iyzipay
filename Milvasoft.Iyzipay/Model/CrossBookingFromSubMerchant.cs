using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class CrossBookingFromSubMerchant : IyzipayResource
    {

        private readonly IRestHttpClient _restHttpClient;

        public CrossBookingFromSubMerchant(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<CrossBookingFromSubMerchant> CreateAsync(CreateCrossBookingRequest request)
        {
            return await _restHttpClient.PostAsync<CrossBookingFromSubMerchant>("/crossbooking/receive", request).ConfigureAwait(false);
        }
    }
}
