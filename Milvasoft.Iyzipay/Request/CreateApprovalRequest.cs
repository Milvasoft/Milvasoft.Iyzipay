﻿using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request
{
    public class CreateApprovalRequest : BaseRequest
    {
        public string PaymentTransactionId { get; set; }

        public override string ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .AppendSuper(base.ToPKIRequestString())
                .Append("paymentTransactionId", PaymentTransactionId)
                .GetRequestString();
        }
    }
}
