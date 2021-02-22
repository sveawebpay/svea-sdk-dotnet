using System;
using System.Collections.Generic;

namespace Svea.WebPay.SDK.CheckoutApi
{
    using System.Globalization;

    public class CreateOrderModel
    {
        /// <summary>
        /// CreateOrderModel
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="currency"></param>
        /// <param name="locale">
        /// <summary>
        /// Locale for the order
        /// </summary>
        /// <remarks>Supported: sv-se, da-dk, de-de, en-us, fi-fi, nn-no.</remarks>
        /// </param>
        /// <param name="clientOrderNumber">
        /// <summary>
        /// A string that identifies the order in the merchant’s systems. The ClientOrderNumber is unique per order.
        /// Attempting to create a new order with a previously used ClientOrderNumber will result in the create method returning the already existing order instead.
        /// </summary>
        /// <remarks>Max length: 32.</remarks>
        /// </param>
        /// <param name="merchantSettings">
        /// <summary>
        /// At certain points in an order’s lifetime, we will attempt to call endpoints on your side. You provide the URI:s for these endpoints in the MerchantSettings object
        /// </summary>
        /// </param>
        /// <param name="cart"></param>
        /// <param name="requireElectronicIdAuthentication"></param>
        /// <param name="presetValues"></param>
        /// <param name="identityFlags"></param>
        /// <param name="partnerKey">
        /// <summary>
        /// Provided by Svea to select partners.
        /// </summary>
        /// <remarks>Optional.</remarks>
        /// </param>
        /// <param name="merchantData">
        /// <summary>
        /// Metadata visible to the store
        /// </summary>
        /// <remarks>Max length: 6000. Optional. Cleaned up from Checkout database after 45 days.</remarks>
        /// </param>
        public CreateOrderModel(RegionInfo countryCode, CurrencyCode currency, Language locale, string clientOrderNumber,
            MerchantSettings merchantSettings, Cart cart, bool requireElectronicIdAuthentication, IList<Presetvalue> presetValues = null,
            IdentityFlags identityFlags = null, Guid? partnerKey = null, string merchantData = null)
        {
            CountryCode = countryCode;
            Currency = currency;
            Locale = locale;
            ClientOrderNumber = clientOrderNumber;
            MerchantSettings = merchantSettings;
            Cart = cart;
            RequireElectronicIdAuthentication = requireElectronicIdAuthentication;
            PresetValues = presetValues;
            IdentityFlags = identityFlags;
            PartnerKey = partnerKey;
            MerchantData = merchantData;
        }

        public RegionInfo CountryCode { get; }
        public CurrencyCode Currency { get; }

        /// <summary>
        /// Locale for the order
        /// </summary>
        /// <remarks>Supported: sv-se, da-dk, de-de, en-us, fi-fi, nn-no.</remarks>
        public Language Locale { get; }

        /// <summary>
        /// A string that identifies the order in the merchant’s systems. The ClientOrderNumber is unique per order.
        /// Attempting to create a new order with a previously used ClientOrderNumber will result in the create method returning the already existing order instead.
        /// </summary>
        /// <remarks>Max length: 32.</remarks>
        public string ClientOrderNumber { get; }

        /// <summary>
        /// At certain points in an order’s lifetime, we will attempt to call endpoints on your side. You provide the URI:s for these endpoints in the MerchantSettings object
        /// </summary>
        public MerchantSettings MerchantSettings { get; }

        public Cart Cart { get; }
        public bool RequireElectronicIdAuthentication { get; }
        public IList<Presetvalue> PresetValues { get; }
        public IdentityFlags IdentityFlags { get; }

        /// <summary>
        /// Provided by Svea to select partners.
        /// </summary>
        /// <remarks>Optional.</remarks>
        public Guid? PartnerKey { get; }

        /// <summary>
        /// Metadata visible to the store
        /// </summary>
        /// <remarks>Max length: 6000. Optional. Cleaned up from Checkout database after 45 days.</remarks>
        public string MerchantData { get; }
    }
}