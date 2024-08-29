using System;

namespace Svea.WebPay.SDK.CheckoutApi.Recurring
{
    public class RecurringMerchantSettings
    {
        /// <summary>
        /// An optional URI to a location that expects callbacks from the Checkout to validate an order’s stock status It also has the possibility to update the checkout with an updated ClientOrderNumber.
        /// May contain a {checkout.order.uri} placeholder which will be replaced with the checkoutorderid.
        /// Requests for this endpoint are made with HTTP Method GET. Your response's HTTP Status Code is interpreted as:
        /// 200-299 is interpreted as validation passed.
        /// Everything else is interpreted as validation failure.
        /// </summary>
        /// <remarks>Data type: Url. Max length: 500.</remarks>
        public Uri CheckoutValidationCallBackUri { get; set; }


        /// <summary>
        /// URI to a location that expects callbacks from the Checkout whenever an order’s state is changed (confirmed, final, etc.).
        /// May contain a {checkout.order.uri} placeholder which will be replaced with the checkoutorderid.
        /// Requests for this endpoint are made with HTTP Method POST. Your response's HTTP Status Code is interpreted as:
        /// 100-199 are ignored.
        /// 200-299 is interpreted as OK.
        /// 300-399 are ignored.
        /// 404 - the order hasn't been created on your side yet. We will try pushing again. All other 400 status codes are ignored.
        /// 500 and above is interpreted as error on your side. We will try pushing again.
        /// </summary>
        /// <remarks>Required. Data type: Url. Max length: 500. Min length: 1.</remarks>
        public Uri PushUri { get; set; }
    }
}