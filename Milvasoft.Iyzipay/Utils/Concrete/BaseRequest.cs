﻿namespace Milvasoft.Iyzipay.Utils.Concrete
{
    public class BaseRequest : IRequestStringConvertible
    {
        public string Locale { get; set; }
        public string ConversationId { get; set; }

        public virtual string ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .Append("locale", Locale)
                .Append("conversationId", ConversationId)
                .GetRequestString();
        }
    }
}
