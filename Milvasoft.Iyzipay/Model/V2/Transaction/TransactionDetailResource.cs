using Milvasoft.Iyzipay.Utils.Concrete;
using System.Collections.Generic;

namespace Milvasoft.Iyzipay.Model.V2.Transaction
{
    public class TransactionDetailResource : IyzipayResourceV2
    {
        public List<TransactionDetailItem> Payments { get; set; }
    }
}
