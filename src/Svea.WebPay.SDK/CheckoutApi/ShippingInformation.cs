namespace Svea.WebPay.SDK.CheckoutApi
{
    using System.Collections.Generic;

    public class ShippingInformation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enforceFallback">In order to test how your fallback options will be demonstrated (Only for testing purposes)</param>
        /// <param name="enableShipping">If Shipping is enabled at Svea for the specific merchant and the property enableShipping = false then shipping will be disabled for that order (for example if the order is digital then the shipping can be disabled using this parameter, however merchant can also configure digital items in nShift for example if weight is set to 0)</param>
        /// <param name="weight">Weight of the parcel in grams</param>
        /// <param name="tags"></param>
        /// <param name="fallbackOptions"></param>
        public ShippingInformation(bool enforceFallback, bool enableShipping, int weight, Dictionary<string, string> tags, ShippingOption fallbackOptions)
        {
            EnforceFallback = enforceFallback;
            EnableShipping = enableShipping;
            Weight = weight;
            Tags = tags;
            FallbackOptions = fallbackOptions;
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
        public int Weight { get; }

        public Dictionary<string, string> Tags { get; }

        public ShippingOption FallbackOptions { get; }
    }
}