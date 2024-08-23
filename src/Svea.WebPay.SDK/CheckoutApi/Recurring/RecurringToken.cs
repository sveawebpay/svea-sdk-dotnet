using System.Text.Json.Serialization;

namespace Svea.WebPay.SDK.CheckoutApi.Recurring
{
    public class RecurringToken
    {

        /// <summary>
        /// ID of the token, needed to get the token data or create a subsequent order
        /// </summary>
        [JsonInclude]
        public string Token { get; set; }

        /// <summary>
        /// Status for recurring token
        /// </summary>
        [JsonInclude]
        public TokenStatus Status { get; set; }

        /// <summary>
        /// Currency as defined by ISO 4217
        /// </summary>
        [JsonInclude]
        public string Currency { get; set; }

        /// <summary>
        /// The payment method associated with the token.
        ///     * Invoice
        ///     * Card
        /// </summary>
        [JsonInclude]
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Details specific to chosen payment method
        /// </summary>
        [JsonInclude]
        public PaymentMethodDetails PaymentMethodDetails { get; set; }
    }
    public sealed class PaymentMethodDetails
    {
        /// <summary>
        /// Month indicating when the card will expire
        /// </summary>
        [JsonInclude]
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// Year indicating when the card will expire
        /// </summary>
        [JsonInclude]
        public int ExpiryYear { get; set; }
    }
}