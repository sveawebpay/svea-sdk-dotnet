using System;

namespace Svea.WebPay.SDK.CheckoutApi.Recurring
{
    public class ChangePaymentMethodResponse
    {
        /// <summary>
        /// String representing HTML snippet for changing payment methods:
        /// Example: "<iframe src=\"\"></iframe>"
        /// </summary>
        public string Snippet { get; set; }

        /// <summary>
        /// DateTime representing when payment methods change request expires.
        /// In string form example: "2023-12-01T19:01:55.714942"
        /// </summary>
        public DateTime Expiration { get; set; }
    }
}