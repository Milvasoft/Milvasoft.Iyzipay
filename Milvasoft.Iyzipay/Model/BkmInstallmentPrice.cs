using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Model
{
    public class BkmInstallmentPrice : IRequestStringConvertible
    {
        public int? InstallmentNumber { get; set; }
        public string TotalPrice { get; set; }

        public string ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .Append("installmentNumber", InstallmentNumber)
                .AppendPrice("totalPrice", TotalPrice)
                .GetRequestString();
        }
    }
}
