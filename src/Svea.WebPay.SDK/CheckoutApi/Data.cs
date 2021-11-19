namespace Svea.WebPay.SDK.CheckoutApi
{
    using Svea.WebPay.SDK.Json;

    using System.Text.Json.Serialization;

    public class Data
    {
        public Data() { }

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
        public Data(MerchantSettings merchantSettings, CartResponse cart, Customer customer, Address shippingAddress, Address billingAddress, Gui gui, string locale, string currency, 
            string countryCode, Presetvalue[] presetValues, string clientOrderNumber, long orderId, string emailAddress, string phoneNumber, PaymentType? paymentType, 
            CheckoutOrderStatus status, object customerReference, bool? sveaWillBuyOrder, IdentityFlags identityFlags, object merchantData, PaymentInfo payment, string peppolId)
        {
            MerchantSettings = merchantSettings;
            Cart = cart;
            Customer = customer;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Gui = gui;
            Locale = locale;
            Currency = currency;
            CountryCode = countryCode;
            PresetValues = presetValues;
            ClientOrderNumber = clientOrderNumber;
            OrderId = orderId;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            PaymentType = paymentType ?? CheckoutApi.PaymentType.Unknown;
            Status = status;
            CustomerReference = customerReference;
            SveaWillBuyOrder = sveaWillBuyOrder;
            IdentityFlags = identityFlags;
            MerchantData = merchantData;
            Payment = payment;
            PeppolId = peppolId;
        }

        /// <summary>
        /// Specific merchant URIs
        /// </summary>
        /// <remarks>Required.</remarks>
        [JsonInclude]
        public MerchantSettings MerchantSettings { get; }

        /// <summary>
        /// Order rows.
        /// </summary>
        [JsonInclude]
        public CartResponse Cart { get; }

        /// <summary>
        /// Identified customer of the order.
        /// </summary>
        [JsonInclude]
        public Customer Customer { get; }

        /// <summary>
        /// Shipping address of identified customer.
        /// </summary>
        [JsonInclude]
        public Address ShippingAddress { get; }

        /// <summary>
        /// Billing address of identified customer.
        /// </summary>
        [JsonInclude]
        public Address BillingAddress { get; }

        [JsonInclude]
        public Gui Gui { get; }

        /// <summary>
        /// The current locale of the checkout, i.e.sv-SE etc.
        /// </summary>
        /// <remarks>Required.</remarks>
        [JsonInclude]
        public string Locale { get; }

        /// <summary>
        /// The current currency as defined by ISO 4217, i.e. SEK, NOK etc.
        /// </summary>
        /// <remarks>Required.</remarks>
        [JsonInclude]
        public string Currency { get; }

        /// <summary>
        /// Defined by two-letter ISO 3166-1 alpha-2, i.e. SE, DE, FI etc.
        /// </summary>
        [JsonInclude]
        public string CountryCode { get; }

        /// <summary>
        /// A list containing the preset values, if any.
        /// </summary>
        [JsonInclude]
        public Presetvalue[] PresetValues { get; }

        [JsonInclude]
        public string ClientOrderNumber { get; }
        
        [JsonInclude]
        public long OrderId { get; }
        
        [JsonInclude]
        public string EmailAddress { get; }

        /// <summary>
        /// The customer’s phone number
        /// </summary>
        [JsonInclude]
        public string PhoneNumber { get; }

        /// <summary>
        /// The final payment method for the order. Will only have a value when the order is finalized, otherwise unknown.
        /// </summary>
        [JsonInclude]
        public PaymentType? PaymentType { get;  }

        /// <summary>
        /// The current state of the order
        /// </summary>
        [JsonInclude]
        public CheckoutOrderStatus Status { get; }

        /// <summary>
        /// B2B Customer reference
        /// </summary>
        [JsonInclude]
        public object CustomerReference { get; }

        /// <summary>
        /// True = Svea will buy this invoice. False = Svea will not buy this invoice. null = Selected payment method is not Invoice.
        /// </summary>
        [JsonInclude]
        public bool? SveaWillBuyOrder { get; }

        [JsonInclude]
        public IdentityFlags IdentityFlags { get; }

        /// <summary>
        /// Metadata visible to the store
        /// </summary>
        /// <remarks>Optional. Cleaned up from Checkout database after 45 days.</remarks>
        [JsonInclude]
        public object MerchantData { get; }

        /// <summary>
        /// The final payment method for the order. Will only have a value when the order is finalized, otherwise null.
        /// </summary>
        [JsonInclude]
        public PaymentInfo Payment { get; }

        /// <summary>
        /// A company’s ID in the PEPPOL network, which allows the company to receive PEPPOL invoices. A PEPPOL ID can be entered when placing a B2B order using the payment method invoice.
        /// </summary>
        [JsonInclude]
        public string PeppolId { get; }
    }
}