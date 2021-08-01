using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request.V2.Subscription
{
    public class UpdatePlanRequest : BaseRequestV2
    {
        public string PricingPlanReferenceCode { get; set; }
        public string Name { get; set; }
        public int? TrialPeriodDays { get; set; }
    }
}