using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request.V2.Subscription
{
    public class RetrieveCustomerRequest : BaseRequestV2
    {
        public string CustomerReferenceCode { get; set; }
    }
}