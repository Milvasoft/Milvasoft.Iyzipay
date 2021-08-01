﻿using Newtonsoft.Json;

namespace Milvasoft.Iyzipay.Model.V2.Transaction
{
    public class TransactionReportItem
    {
        public string TransactionType { get; set; }
        public string TransactionDate { get; set; }
        public long TransactionId { get; set; }
        public int AfterSettlement { get; set; }

        [JsonProperty(PropertyName = "paymentTxId")]
        public long PaymentTransactionId { get; set; }
        public long PaymentId { get; set; }
        public string ConversationId { get; set; }
        public string PaymentPhase { get; set; }
        public string Price { get; set; }
        public string PaidPrice { get; set; }
        public string TransactionCurrency { get; set; }
        public int Installment { get; set; }
        public int ThreeDS { get; set; }
        public string IyzicoCommission { get; set; }
        public string IyzicoFee { get; set; }
        public string Parity { get; set; }
        public string IyzicoConversionAmount { get; set; }
        public string SettlementCurrency { get; set; }
        public string MerchantPayoutAmount { get; set; }
        public string ConnectorType { get; set; }
        public string PosOrderId { get; set; }
        public string PosName { get; set; }
        public string SubMerchantKey { get; set; }
        public string SubMerchantPayoutAmount { get; set; }
        public string AuthCode { get; set; }
        public string HostReference { get; set; }
        public string BasketId { get; set; }
        public int? TransactionStatus { get; set; }
    }
}
