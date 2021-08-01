using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class Disapproval : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public string PaymentTransactionId { get; set; }


        public Disapproval(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<Disapproval> CreateAsync(CreateApprovalRequest request)
        {
            return await _restHttpClient.PostAsync<Disapproval>("/payment/iyzipos/item/disapprove", request).ConfigureAwait(false);
        }
    }
}
