namespace Svea.WebPay.SDK
{
    using System.Text.Json.Serialization;

    public class MinorUnit
    {
        [JsonConstructor]
        private MinorUnit(decimal value)
        {
            Value = decimal.ToInt32(value);
        }


        internal MinorUnit(long value)
        {
            Value = value;
        }


        public long Value { get; }


        public static MinorUnit FromDecimal(decimal amount)
        {
            return new MinorUnit(amount * 100);
        }


        public static MinorUnit FromInt(long amount)
        {
            return new MinorUnit(amount * 100);
        }


        public static decimal ToDecimal(MinorUnit amount)
        {
            return (decimal)amount.Value / 100;
        }

        public static int ToInt(MinorUnit amount)
        {
            return (int)amount.Value / 100;
        }


        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
