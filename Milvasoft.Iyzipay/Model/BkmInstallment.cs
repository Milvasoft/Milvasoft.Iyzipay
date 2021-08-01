using Milvasoft.Iyzipay.Utils.Concrete;
using System.Collections.Generic;

namespace Milvasoft.Iyzipay.Model
{
    public class BkmInstallment : IRequestStringConvertible
    {
        public long? BankId { get; set; }
        public List<BkmInstallmentPrice> InstallmentPrices { get; set; }

        public string ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .Append("bankId", BankId)
                .AppendList("installmentPrices", InstallmentPrices)
                .GetRequestString();
        }
    }
}
