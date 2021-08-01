using Newtonsoft.Json;
using System.Collections.Generic;

namespace Milvasoft.Iyzipay.Model.V2.Subscription
{
    public class SubscriptionResource : SubscriptionCreatedResource
    {
        public string CustomerEmail { get; set; }

        [JsonProperty(PropertyName = "orders")]
        public List<SubscriptionOrder> SubscriptionOrders { get; set; }
    }
}