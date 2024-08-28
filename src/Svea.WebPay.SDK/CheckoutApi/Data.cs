namespace Svea.WebPay.SDK.CheckoutApi
{
    using Svea.WebPay.SDK.CheckoutApi.Response;
    using System;
    using System.Text.Json.Serialization;

    public class Data : OrderData
    {
        public Data() : base() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantSettings">
        /// <summary>
        /// Specific merchant URIs
        /// </summary>
        /// <remarks>Required.</remarks>
        /// </param>
        /// <param name="cart">
        /// <summary>
        /// Order rows.
        /// </summary>
        /// </param>
        /// <param name="customer">
        /// <summary>
        /// Identified customer of the order.
        /// </summary>
        /// </param>
        /// <param name="shippingAddress">
        /// <summary>
        /// Shipping address of identified customer.
        /// </summary>
        /// </param>
        /// <param name="billingAddress">
        /// <summary>
        /// Billing address of identified customer.
        /// </summary>
        /// </param>
        /// <param name="gui"></param>
        /// <param name="locale">
        /// <summary>
        /// The current locale of the checkout, i.e.sv-SE etc.
        /// </summary>
        /// <remarks>Required.</remarks>
        /// </param>
        /// <param name="currency">
        /// <summary>
        /// The current currency as defined by ISO 4217, i.e. SEK, NOK etc.
        /// </summary>
        /// <remarks>Required.</remarks>
        /// </param>
        /// <param name="countryCode">
        /// <summary>
        /// Defined by two-letter ISO 3166-1 alpha-2, i.e. SE, DE, FI etc.
        /// </summary>
        /// </param>
        /// <param name="presetValues">
        /// <summary>
        /// A list containing the preset values, if any.
        /// </summary>
        /// </param>
        /// <param name="clientOrderNumber"></param>
        /// <param name="orderId"></param>
        /// <param name="emailAddress"></param>
        /// <param name="phoneNumber">
        /// <summary>
        /// The customer’s phone number
        /// </summary>
        /// </param>
        /// <param name="paymentType">
        /// <summary>
        /// The final payment method for the order. Will only have a value when the order is finalized, otherwise unknown.
        /// </summary>
        /// </param>
        /// <param name="status">
        /// <summary>
        /// The current state of the order
        /// </summary>
        /// </param>
        /// <param name="customerReference">
        /// <summary>
        /// B2B Customer reference
        /// </summary>
        /// </param>
        /// <param name="sveaWillBuyOrder">
        /// <summary>
        /// True = Svea will buy this invoice. False = Svea will not buy this invoice. null = Selected payment method is not Invoice.
        /// </summary>
        /// </param>
        /// <param name="identityFlags"></param>
        /// <param name="merchantData">
        /// <summary>
        /// Metadata visible to the store
        /// </summary>
        /// <remarks>Optional. Cleaned up from Checkout database after 45 days.</remarks>
        /// </param>
        /// <param name="payment">
        /// <summary>
        /// The final payment method for the order. Will only have a value when the order is finalized, otherwise null.
        /// </summary>
        /// </param>
        /// <param name="peppolId">
        /// <summary>
        /// A company’s ID in the PEPPOL network, which allows the company to receive PEPPOL invoices. A PEPPOL ID can be entered when placing a B2B order using the payment method invoice.
        /// </summary>
        /// </param>
        [JsonConstructor]
        public Data(MerchantSettings merchantSettings, Cart cart, Customer customer, Address shippingAddress, Address billingAddress, Gui gui, string locale, string currency,
            string countryCode, Presetvalue[] presetValues, string clientOrderNumber, long orderId, string emailAddress, string phoneNumber, PaymentType? paymentType,
            CheckoutOrderStatus status, object customerReference, bool? sveaWillBuyOrder, IdentityFlags identityFlags, object merchantData, PaymentInfo payment, string peppolId, GetOrderShippingInformation shippingInformation,
            bool? recurring, string recurringToken = null) : base(merchantSettings, cart, customer, shippingAddress, billingAddress, locale, currency, countryCode,
                presetValues, clientOrderNumber, orderId, emailAddress, phoneNumber, paymentType, status, customerReference,
                sveaWillBuyOrder, identityFlags, merchantData, payment, peppolId, shippingInformation, recurring, recurringToken)
        {

            Gui = gui;
        }


        [JsonInclude]
        public Gui Gui { get; }


    }
}