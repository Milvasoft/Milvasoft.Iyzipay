﻿using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Model
{
    public class CardInformation : IRequestStringConvertible
    {
        public string CardAlias { get; set; }
        public string CardNumber { get; set; }
        public string ExpireYear { get; set; }
        public string ExpireMonth { get; set; }
        public string CardHolderName { get; set; }

        public string ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .Append("cardAlias", CardAlias)
                .Append("cardNumber", CardNumber)
                .Append("expireYear", ExpireYear)
                .Append("expireMonth", ExpireMonth)
                .Append("cardHolderName", CardHolderName)
                .GetRequestString();
        }
    }
}
