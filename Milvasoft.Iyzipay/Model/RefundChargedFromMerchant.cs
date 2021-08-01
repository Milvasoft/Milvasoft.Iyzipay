using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class RefundChargedFromMerchant : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public string PaymentId { get; set; }
        public string PaymentTransactionId { get; set; }
        public string Price { get; set; }
        public string AuthCode { get; set; }
        public string HostReference { get; set; }


        public RefundChargedFromMerchant(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }


        public async Task<RefundChargedFromMerchant> CreateAsync(CreateRefundRequest request)
        {
            return await _restHttpClient.PostAsync<RefundChargedFromMerchant>("/payment/iyzipos/refund/merchant/charge", request).ConfigureAwait(false);
        }
    }
}
