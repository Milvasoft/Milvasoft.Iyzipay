using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Request
{
    public class CreateCardRequest : BaseRequest
    {
        public string ExternalId { get; set; }
        public string Email { get; set; }
        public string CardUserKey { get; set; }
        public CardInformation Card { get; set; }

        public override string ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .AppendSuper(base.ToPKIRequestString())
                .Append("externalId", ExternalId)
                .Append("email", Email)
                .Append("cardUserKey", CardUserKey)
                .Append("card", Card)
                .GetRequestString();
        }
    }
}
