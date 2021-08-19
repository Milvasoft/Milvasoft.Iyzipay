using System;
using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request.V2.Subscription
{
    public class RetrieveCheckoutFormResultRequest : BaseRequestV2
    {
        public String Token { get; set; }
    }
}