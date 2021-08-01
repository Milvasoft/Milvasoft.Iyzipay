using Milvasoft.Iyzipay.Utils.Concrete;
using Newtonsoft.Json;

namespace Milvasoft.Iyzipay.Model
{
    public class Address : IRequestStringConvertible
    {
        [JsonProperty(PropertyName = "Address")]
        public string Description { get; set; }
        public string ZipCode { get; set; }
        public string ContactName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .Append("address", Description)
                .Append("zipCode", ZipCode)
                .Append("contactName", ContactName)
                .Append("city", City)
                .Append("country", Country)
                .GetRequestString();
        }
    }
}
