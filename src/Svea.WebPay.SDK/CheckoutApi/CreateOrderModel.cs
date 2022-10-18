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
        /// Locale for the order
        /// <remarks>Supported: sv-se, da-dk, de-de, en-us, fi-fi, nn-no.</remarks>
        /// </param>
        /// <param name="clientOrderNumber">
        /// A string that identifies the order in the merchant’s systems. The ClientOrderNumber is unique per order.
        /// Attempting to create a new order with a previously used ClientOrderNumber will result in the create method returning the already existing order instead.
        /// <remarks>Max length: 32.</remarks>
        /// </param>
        /// <param name="merchantSettings">
        /// At certain points in an order’s lifetime, we will attempt to call endpoints on your side. You provide the URI:s for these endpoints in the MerchantSettings object
        /// </param>
        /// <param name="cart"></param>
        /// <param name="requireElectronicIdAuthentication"></param>
        /// <param name="presetValues"></param>
        /// <param name="identityFlags"></param>
        /// <param name="partnerKey">
        /// Provided by Svea to select partners.
        /// <remarks>Optional.</remarks>
        /// </param>
        /// <param name="merchantData">
        /// Metadata visible to the store
        /// <remarks>Max length: 6000. Optional. Cleaned up from Checkout database after 45 days.</remarks>
        /// </param>
        /// <param name="shippingInformation">Shipping information needed for the shipping checkout. Only applicable if merchant has shipping enabled.</param>
        public CreateOrderModel(RegionInfo countryCode, CurrencyCode currency, Language locale, string clientOrderNumber,
            MerchantSettings merchantSettings, Cart cart, bool requireElectronicIdAuthentication, IList<Presetvalue> presetValues = null,
            IdentityFlags identityFlags = null, Guid? partnerKey = null, string merchantData = null, ShippingInformation shippingInformation = null)
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
            ShippingInformation = shippingInformation;
        }

        /// <summary>
        /// CreateOrderModel
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="currency"></param>
        /// <param name="locale">
        /// Locale for the order
        /// <remarks>Supported: sv-se, da-dk, de-de, en-us, fi-fi, nn-no.</remarks>
        /// </param>
        /// <param name="clientOrderNumber">
        /// A string that identifies the order in the merchant’s systems. The ClientOrderNumber is unique per order.
        /// Attempting to create a new order with a previously used ClientOrderNumber will result in the create method returning the already existing order instead.
        /// <remarks>Max length: 32.</remarks>
        /// </param>
        /// <param name="merchantSettings">
        /// At certain points in an order’s lifetime, we will attempt to call endpoints on your side. You provide the URI:s for these endpoints in the MerchantSettings object
        /// </param>
        /// <param name="cart"></param>
        /// <param name="requireElectronicIdAuthentication"></param>
        /// <param name="shippingInformation">Shipping information needed for the shipping checkout. Only applicable if merchant has shipping enabled.</param>
        public CreateOrderModel(RegionInfo countryCode, CurrencyCode currency, Language locale, string clientOrderNumber,
         MerchantSettings merchantSettings, Cart cart, bool requireElectronicIdAuthentication, ShippingInformation shippingInformation)
        {
            CountryCode = countryCode;
            Currency = currency;
            Locale = locale;
            ClientOrderNumber = clientOrderNumber;
            MerchantSettings = merchantSettings;
            Cart = cart;
            RequireElectronicIdAuthentication = requireElectronicIdAuthentication;
            ShippingInformation = shippingInformation;
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

        /// <summary>
        /// Shipping information needed for the shipping checkout. Only applicable if merchant has shipping enabled.	
        /// </summary>
        public ShippingInformation ShippingInformation { get; }
    }
}