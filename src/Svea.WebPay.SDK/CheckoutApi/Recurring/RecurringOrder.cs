using System.Text.Json.Serialization;

namespace Svea.WebPay.SDK.CheckoutApi.Recurring
{
    public class RecurringOrder
    {
        /// <summary>
        /// Specific merchant URIs
        /// </summary>
        /// <remarks>Required.</remarks>
        [JsonInclude]
        public RecurringMerchantSettings MerchantSettings { get; set; }

        /// <summary>
        /// Order rows.
        /// </summary>
        [JsonInclude]
        public Cart Cart { get; set; }

        /// <summary>
        /// Identified customer of the order.
        /// </summary>
        [JsonInclude]
        public Customer Customer { get; set; }

        /// <summary>
        /// Shipping address of identified customer.
        /// </summary>
        [JsonInclude]
        public Address ShippingAddress { get; set; }

        /// <summary>
        /// Billing address of identified customer.
        /// </summary>
        [JsonInclude]
        public Address BillingAddress { get; set; }


        /// <summary>
        /// The current locale of the checkout, i.e.sv-SE etc.
        /// </summary>
        /// <remarks>Required.</remarks>
        [JsonInclude]
        public string Locale { get; set; }

        /// <summary>
        /// The current currency as defined by ISO 4217, i.e. SEK, NOK etc.
        /// </summary>
        /// <remarks>Required.</remarks>
        [JsonInclude]
        public string Currency { get; set; }

        /// <summary>
        /// Defined by two-letter ISO 3166-1 alpha-2, i.e. SE, DE, FI etc.
        /// </summary>
        [JsonInclude]
        public string CountryCode { get; set; }

        /// <summary>
        /// A list containing the preset values, if any.
        /// </summary>
        [JsonInclude]
        public Presetvalue[] PresetValues { get; set; }

        [JsonInclude]
        public string ClientOrderNumber { get; set; }

        [JsonInclude]
        public long OrderId { get; set; }

        [JsonInclude]
        public string EmailAddress { get; set; }

        /// <summary>
        /// The customer’s phone number
        /// </summary>
        [JsonInclude]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The final payment method for the order. Will only have a value when the order is finalized, otherwise unknown.
        /// </summary>
        [JsonInclude]
        public PaymentType? PaymentType { get; set; }

        /// <summary>
        /// The current state of the order
        /// </summary>
        [JsonInclude]
        public CheckoutOrderStatus Status { get; set; }

        /// <summary>
        /// B2B Customer reference
        /// </summary>
        [JsonInclude]
        public object CustomerReference { get; set; }

        /// <summary>
        /// True = Svea will buy this invoice. False = Svea will not buy this invoice. null = Selected payment method is not Invoice.
        /// </summary>
        [JsonInclude]
        public bool? SveaWillBuyOrder { get; set; }

        [JsonInclude]
        public IdentityFlags IdentityFlags { get; set; }

        /// <summary>
        /// Metadata visible to the store
        /// </summary>
        /// <remarks>Optional. Cleaned up from Checkout database after 45 days.</remarks>
        [JsonInclude]
        public object MerchantData { get; set; }

        /// <summary>
        /// The final payment method for the order. Will only have a value when the order is finalized, otherwise null.
        /// </summary>
        [JsonInclude]
        public PaymentInfo Payment { get; set; }

        /// <summary>
        /// A company’s ID in the PEPPOL network, which allows the company to receive PEPPOL invoices. A PEPPOL ID can be entered when placing a B2B order using the payment method invoice.
        /// </summary>
        [JsonInclude]
        public string PeppolId { get; set; }

        [JsonInclude]
        public GetOrderShippingInformation ShippingInformation { get; set; }

        /// <summary>
        /// Indicates if the order is recurring order and will create a recurring token when order is finalized. Only applicable if merchant has recurring orders enabled.
        /// </summary>
        [JsonInclude]
        public bool? Recurring { get; set; }


        /// <summary>
        /// Recurring token to be used for subsequent recurring orders. Only available when order is finalized. Only applicable if merchant has recurring orders enabled.
        /// </summary>
        [JsonInclude]
        public string RecurringToken { get; set; }

        /// <summary>
        /// Order validations such as minimum age requirement.
        /// Apply it in order to have order validation such as minimum age.
        /// </summary>
        [JsonInclude]
        public OrderValidation Validation { get; set; }
    }
}
