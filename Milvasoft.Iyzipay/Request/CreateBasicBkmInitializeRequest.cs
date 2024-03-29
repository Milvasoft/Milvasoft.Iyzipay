﻿using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Collections.Generic;

namespace Milvasoft.Iyzipay.Request
{
    public class CreateBasicBkmInitializeRequest : BaseRequest
    {
        public string ConnectorName { get; set; }
        public string Price { get; set; }
        public string CallbackUrl { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerId { get; set; }
        public string BuyerIp { get; set; }
        public string PosOrderId { get; set; }
        public List<BkmInstallment> InstallmentDetails { get; set; }

        public override string ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .AppendSuper(base.ToPKIRequestString())
                .Append("connectorName", ConnectorName)
                .AppendPrice("price", Price)
                .Append("callbackUrl", CallbackUrl)
                .Append("buyerEmail", BuyerEmail)
                .Append("buyerId", BuyerId)
                .Append("buyerIp", BuyerIp)
                .Append("posOrderId", PosOrderId)
                .AppendList("installmentDetails", InstallmentDetails)
                .GetRequestString();
        }
    }
}
