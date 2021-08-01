using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class Approval : IyzipayResource
    {
        public string PaymentTransactionId { get; set; }


        private readonly IRestHttpClient _restHttpClient;

        public Approval(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<Approval> CreateAsync(CreateApprovalRequest request)
        {
            return await _restHttpClient.PostAsync<Approval>("/payment/iyzipos/item/approve", request).ConfigureAwait(false);
        }
    }
}
