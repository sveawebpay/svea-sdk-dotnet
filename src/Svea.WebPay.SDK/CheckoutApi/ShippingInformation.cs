namespace Svea.WebPay.SDK.CheckoutApi
{
    using System.Collections.Generic;

    public class ShippingInformation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enableShipping">If Shipping is enabled at Svea for the specific merchant and the property enableShipping = false then shipping will be disabled for that order (for example if the order is digital then the shipping can be disabled using this parameter, however merchant can also configure digital items in nShift for example if weight is set to 0)</param>
        /// <param name="weight">Weight of the parcel in grams</param>
        /// <param name="tags"></param>
        /// <param name="fallbackOptions">A list of fallback options</param>
        /// <param name="enforceFallback">In order to test how your fallback options will be demonstrated (Only for testing purposes)</param>
        public ShippingInformation(bool enableShipping, double weight, Dictionary<string, string> tags, List<FallbackOption> fallbackOptions, bool enforceFallback = false)
        {
            EnableShipping = enableShipping;
            Weight = weight;
            Tags = tags;
            FallbackOptions = fallbackOptions;
            EnforceFallback = enforceFallback;
        }

        /// <summary>
        /// In order to test how your fallback options will be demonstrated (Only for testing purposes)
        /// </summary>
        public bool EnforceFallback { get; }

        /// <summary>
        /// If Shipping is enabled at Svea for the specific merchant and the property enableShipping = false then shipping will be disabled for that order (for example if the order is digital then the shipping can be disabled using this parameter, however merchant can also configure digital items in nShift for example if weight is set to 0)
        /// </summary>
        public bool EnableShipping { get; }

        /// <summary>
        /// Weight of the parcel in grams
        /// </summary>
        public double Weight { get; }

        public Dictionary<string, string> Tags { get; }

        /// <summary>
        /// A list of fallback options
        /// </summary>
        public List<FallbackOption> FallbackOptions { get; }
    }
}