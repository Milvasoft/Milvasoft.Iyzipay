using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request.V2.Subscription
{
    public class CancelSubscriptionRequest : BaseRequestV2
    {
        public string SubscriptionReferenceCode { get; set; }
    }
}