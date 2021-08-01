using Milvasoft.Iyzipay.Utils.Concrete;

namespace Milvasoft.Iyzipay.Model
{
    public class OrderItem : IRequestStringConvertible
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string ItemType { get; set; }
        public string ItemUrl { get; set; }
        public string ItemDescription { get; set; }
        public string Price { get; set; }

        public string ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .Append("id", Id)
                .Append("name", Name)
                .Append("category1", Category1)
                .Append("category2", Category2)
                .Append("itemType", ItemType)
                .Append("itemUrl", ItemUrl)
                .Append("itemDescription", ItemDescription)
                .AppendPrice("price", Price)

                .GetRequestString();
        }
    }
}