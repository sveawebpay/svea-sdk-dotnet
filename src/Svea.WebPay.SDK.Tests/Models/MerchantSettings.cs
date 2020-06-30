namespace Svea.WebPay.SDK.Tests.Models
{
    using System;

    public class MerchantSettings
    {
        public Uri PushUri { get; set; }
        public Uri TermsUri { get; set; }
        public Uri CheckoutUri { get; set; }
        public Uri ConfirmationUri { get; set; }
        public Uri CheckoutValidationCallbackUri { get; set; }
    }
}
