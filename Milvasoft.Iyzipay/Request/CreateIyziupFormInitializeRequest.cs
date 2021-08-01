using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Collections.Generic;

namespace Milvasoft.Iyzipay.Request
{
    public class CreateIyziupFormInitializeRequest : BaseRequest
    {
        public string MerchantOrderId { get; set; }
        public string PaymentGroup { get; set; }
        public string PaymentSource { get; set; }
        public int? ForceThreeDS { get; set; }
        public List<int> EnabledInstallments { get; set; }
        public string EnabledCardFamily { get; set; }
        public string Currency { get; set; }
        public string Price { get; set; }
        public string PaidPrice { get; set; }
        public string ShippingPrice { get; set; }
        public string CallbackUrl { get; set; }
        public string TermsUrl { get; set; }
        public string PreSalesContractUrl { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public InitialConsumer InitialConsumer { get; set; }

        public override string ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .AppendSuper(base.ToPKIRequestString())
                .Append("merchantOrderId", MerchantOrderId)
                .Append("paymentGroup", PaymentGroup)
                .Append("paymentSource", PaymentSource)
                .Append("forceThreeDS", ForceThreeDS)
                .Append("enabledInstallments", EnabledInstallments)
                .Append("enabledCardFamily", EnabledCardFamily)
                .Append("currency", Currency)
                .Append("price", Price)
                .Append("paidPrice", PaidPrice)
                .Append("shippingPrice", ShippingPrice)
                .Append("callbackUrl", CallbackUrl)
                .Append("termsUrl", TermsUrl)
                .Append("preSalesContractUrl", PreSalesContractUrl)
                .Append("orderItems", OrderItems)
                .Append("initialConsumer", InitialConsumer)
                .GetRequestString();
        }
    }
}