using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request.V2.Subscription
{
    public class RetrieveSubscriptionRequest : BaseRequestV2
    {
        public string SubscriptionReferenceCode { get; set; }
    }
}