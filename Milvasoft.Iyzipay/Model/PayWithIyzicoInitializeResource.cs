using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Model
{
    public class PayWithIyzicoInitializeResource : IyzipayResource
    {
        public string Token { get; set; }
        public string CheckoutFormContent { get; set; }
        public long? TokenExpireTime { get; set; }
        public string PayWithIyzicoPageUrl { get; set; }
    }
}
