using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Model
{
    public class IyziupAddress
    {
        public string Alias { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string ContactName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .Append("alias", Alias)
                .Append("addressLine1", AddressLine1)
                .Append("addressLine2", AddressLine2)
                .Append("zipCode", ZipCode)
                .Append("contactName", ContactName)
                .Append("city", City)
                .Append("country", Country)

                .GetRequestString();
        }
    }
}