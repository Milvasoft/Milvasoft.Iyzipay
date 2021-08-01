using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request.V2.Subscription
{
    public class CreateProductRequest : BaseRequestV2
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}