namespace Milvasoft.Iyzipay.Model
{
    public sealed class Currency
    {
        private readonly string value;

        public static readonly Currency TRY = new("TRY");
        public static readonly Currency EUR = new("EUR");
        public static readonly Currency USD = new("USD");
        public static readonly Currency GBP = new("GBP");
        public static readonly Currency IRR = new("IRR");
        public static readonly Currency NOK = new("NOK");
        public static readonly Currency RUB = new("RUB");
        public static readonly Currency CHF = new("CHF");

        private Currency(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }
    }
}
