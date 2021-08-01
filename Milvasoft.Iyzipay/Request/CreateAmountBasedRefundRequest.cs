using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request
{

    public class CreateAmountBasedRefundRequest : BaseRequest
    {
        public string PaymentId { get; set; }
        public string Price { get; set; }
        public string Ip { get; set; }

        public override string ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .AppendSuper(base.ToPKIRequestString())
                .Append("paymentId", PaymentId)
                .AppendPrice("price", Price)
                .Append("ip", Ip)
                .GetRequestString();
        }
    }
}