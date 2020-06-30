using System;

namespace Svea.WebPay.SDK.CheckoutApi
{
    using System.Globalization;

    public class Language
    {
        public Language(string language)
        {
            if (language == null)
            {
                throw new ArgumentNullException(nameof(language));
            }

            Culture = new CultureInfo(language);
            if (Culture.IsNeutralCulture)
            {
                throw new ArgumentException($"Must be given in a xx-YY format.", nameof(language));
            }

            if (!this.IsValid())
            {
                throw new ArgumentException("Supported: sv-se, da-dk, de-de, en-us, fi-fi, nn-no.", nameof(language));
            }
        }

        private CultureInfo Culture { get; }

        public override string ToString()
        {
            return Culture.Name;
        }

        public bool IsValid()
        {
            return (string.Equals(Culture.ToString(), "sv-se", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(Culture.ToString(), "da-dk", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(Culture.ToString(), "de-de", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(Culture.ToString(), "en-us", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(Culture.ToString(), "fi-fi", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(Culture.ToString(), "nn-no", StringComparison.OrdinalIgnoreCase));
        }
    }

}
