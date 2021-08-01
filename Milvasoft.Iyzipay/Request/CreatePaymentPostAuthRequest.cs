﻿using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request
{
    public class CreatePaymentPostAuthRequest : BaseRequest
    {
        public string PaymentId { get; set; }
        public string PaidPrice { get; set; }
        public string Ip { get; set; }
        public string Currency { get; set; }

        public override string ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .AppendSuper(base.ToPKIRequestString())
                .Append("paymentId", PaymentId)
                .Append("ip", Ip)
                .AppendPrice("paidPrice", PaidPrice)
                .Append("currency", Currency)
                .GetRequestString();
        }
    }
}
