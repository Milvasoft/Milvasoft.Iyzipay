using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request.V2
{
    public class RetrieveTransactionDetailRequest : BaseRequestV2
    {
        public string PaymentConversationId { get; set; }
    }
}