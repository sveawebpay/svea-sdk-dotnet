namespace Svea.WebPay.SDK.CheckoutApi
{

    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text.Json.Serialization;

    public class CurrencyCode
    {
        [JsonConstructor]
        public CurrencyCode(string currencyCode)
        {
            if (string.IsNullOrEmpty(currencyCode))
                throw new ArgumentNullException(nameof(currencyCode), "Currency code can't be null or empty");
            if (!IsValidCurrencyCode(currencyCode))
                throw new ArgumentException($"Invalid currency code: {currencyCode}");
            Value = currencyCode;
        }


        private string Value { get; }


        public static bool IsValidCurrencyCode(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                return false;

            var regions = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Where(c => !c.IsNeutralCulture && !string.IsNullOrWhiteSpace(c.Name))
                .Select(culture =>
                {
                    try
                    {
                        return new RegionInfo(culture.Name);
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                });

            return regions.Any(ri => ri != null && ri.ISOCurrencySymbol.Equals(currencyCode));
        }


        public override string ToString()
        {
            return Value;
        }
    }
}

