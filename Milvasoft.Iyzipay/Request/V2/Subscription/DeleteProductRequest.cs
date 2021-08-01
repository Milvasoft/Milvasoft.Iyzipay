using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request.V2.Subscription
{
    public class DeleteProductRequest : BaseRequestV2
    {
        public string ProductReferenceCode { get; set; }
    }
}