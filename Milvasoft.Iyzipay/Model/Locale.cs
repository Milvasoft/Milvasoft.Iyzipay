namespace Milvasoft.Iyzipay.Model
{
    public sealed class Locale
    {
        private readonly string value;

        public static readonly Locale EN = new("en");
        public static readonly Locale TR = new("tr");

        private Locale(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }
    }
}
