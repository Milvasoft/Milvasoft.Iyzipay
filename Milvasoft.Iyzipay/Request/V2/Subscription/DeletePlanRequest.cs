using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request.V2.Subscription
{
    public class DeletePlanRequest : BaseRequestV2
    {
        public string PricingPlanReferenceCode { get; set; }
    }
}