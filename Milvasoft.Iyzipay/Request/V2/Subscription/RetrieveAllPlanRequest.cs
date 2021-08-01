using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request.V2.Subscription
{
    public class RetrieveAllPlanRequest : PagingRequest
    {
        public string ProductReferenceCode { get; set; }
    }
}