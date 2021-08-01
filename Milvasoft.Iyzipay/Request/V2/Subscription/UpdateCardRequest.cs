using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request.V2.Subscription
{
    public class UpdateCardRequest : BaseRequestV2
    {
        public string CustomerReferenceCode { get; set; }
        public string CallbackUrl { get; set; }
    }
}