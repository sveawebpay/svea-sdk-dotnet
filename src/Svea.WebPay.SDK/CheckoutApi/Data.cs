using Newtonsoft.Json;

namespace Svea.WebPay.SDK.CheckoutApi
{
    public class Data
    {
        public Data() { }

        [JsonConstructor]
        public Data(MerchantSettings merchantSettings, Cart cart, Customer customer, Address shippingAddress, Address billingAddress, Gui gui, string locale, string currency, 
            string countryCode, Presetvalue[] presetValues, string clientOrderNumber, long orderId, string emailAddress, string phoneNumber, PaymentType paymentType, 
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
            PaymentType = paymentType;
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
        public MerchantSettings MerchantSettings { get; }

        /// <summary>
        /// Order rows.
        /// </summary>
        public Cart Cart { get; }

        /// <summary>
        /// Identified customer of the order.
        /// </summary>
        public Customer Customer { get; }

        /// <summary>
        /// Shipping address of identified customer.
        /// </summary>
        public Address ShippingAddress { get; }

        /// <summary>
        /// Billing address of identified customer.
        /// </summary>
        public Address BillingAddress { get; }

        public Gui Gui { get; }

        /// <summary>
        /// The current locale of the checkout, i.e.sv-SE etc.
        /// </summary>
        /// <remarks>Required.</remarks>
        public string Locale { get; }

        /// <summary>
        /// The current currency as defined by ISO 4217, i.e. SEK, NOK etc.
        /// </summary>
        /// <remarks>Required.</remarks>
        public string Currency { get; }

        /// <summary>
        /// Defined by two-letter ISO 3166-1 alpha-2, i.e. SE, DE, FI etc.
        /// </summary>
        public string CountryCode { get; }

        /// <summary>
        /// A list containing the preset values, if any.
        /// </summary>
        public Presetvalue[] PresetValues { get; }

        public string ClientOrderNumber { get; }
        public long OrderId { get; }
        public string EmailAddress { get; }

        /// <summary>
        /// The customer’s phone number
        /// </summary>
        public string PhoneNumber { get; }

        /// <summary>
        /// The final payment method for the order. Will only have a value when the order is finalized, otherwise unknown.
        /// </summary>
        public PaymentType PaymentType { get; }

        /// <summary>
        /// The current state of the order
        /// </summary>
        public CheckoutOrderStatus Status { get; }

        /// <summary>
        /// B2B Customer reference
        /// </summary>
        public object CustomerReference { get; }

        /// <summary>
        /// True = Svea will buy this invoice. False = Svea will not buy this invoice. null = Selected payment method is not Invoice.
        /// </summary>
        public bool? SveaWillBuyOrder { get; }

        public IdentityFlags IdentityFlags { get; }

        /// <summary>
        /// Metadata visible to the store
        /// </summary>
        /// <remarks>Optional. Cleaned up from Checkout database after 45 days.</remarks>
        public object MerchantData { get; }

        /// <summary>
        /// The final payment method for the order. Will only have a value when the order is finalized, otherwise null.
        /// </summary>
        public PaymentInfo Payment { get; }

        /// <summary>
        /// A company’s ID in the PEPPOL network, which allows the company to receive PEPPOL invoices. A PEPPOL ID can be entered when placing a B2B order using the payment method invoice.
        /// </summary>
        public string PeppolId { get; }
    }
}