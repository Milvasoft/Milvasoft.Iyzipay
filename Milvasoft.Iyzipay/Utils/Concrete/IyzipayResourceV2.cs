namespace Milvasoft.Iyzipay.Utils.Concrete
{
    public class IyzipayResourceV2
    {
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ConversationId { get; set; }
        public long SystemTime { get; set; }
        public string Locale { get; set; }
    }
}
