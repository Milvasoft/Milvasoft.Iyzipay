using Milvasoft.Iyzipay.Utils.Concrete;
using Newtonsoft.Json;

namespace Milvasoft.Iyzipay.Request.V2.Subscription
{
    public class RetrySubscriptionRequest : BaseRequestV2
    {
        [JsonProperty(PropertyName = "referenceCode")]
        public string SubscriptionOrderReferenceCode { get; set; }
    }
}