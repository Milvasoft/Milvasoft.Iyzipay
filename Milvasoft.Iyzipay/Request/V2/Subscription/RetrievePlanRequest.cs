using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request.V2.Subscription
{
    public class RetrievePlanRequest : BaseRequestV2
    {
        public string PricingPlanReferenceCode { get; set; }
    }
}