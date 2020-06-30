namespace Svea.WebPay.SDK.Extensions
{
    public static class MinorUnitExtensions
    {
        public static decimal ToDecimal(this MinorUnit amount)
        {
            return MinorUnit.ToDecimal(amount);
        }

        public static int ToInt(this MinorUnit amount)
        {
            return MinorUnit.ToInt(amount);
        }
    }
}
