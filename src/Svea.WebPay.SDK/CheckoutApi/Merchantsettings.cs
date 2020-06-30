using System;
using System.Collections.Generic;

namespace Svea.WebPay.SDK.CheckoutApi
{
    public class MerchantSettings
    {
        /// <summary>
        /// MerchantSettings
        /// </summary>
        /// <param name="pushUri">
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
        /// </param>
        /// <param name="termsUri">
        /// <summary>
        /// URI to a page with webshop specific terms.
        /// </summary>
        /// <remarks>Required. Data type: Url. Max length: 500. Min length: 1.</remarks>
        /// </param>
        /// <param name="checkoutUri">
        /// <summary>
        /// URI to the page in the webshop displaying the Checkout. May not contain order specific information.
        /// </summary>
        /// <remarks>Required. Data type: Url. Max length: 500. Min length: 1.</remarks>
        /// </param>
        /// <param name="confirmationUri">
        /// <summary>
        /// URI to the page in the webshop displaying specific information to a customer after the order has been confirmed. May not contain order specific information.
        /// </summary>
        /// <remarks>Required. Data type: Url. Max length: 500. Min length: 1.</remarks>
        /// </param>
        /// <param name="checkoutValidationCallBackUri">
        /// <summary>
        /// An optional URI to a location that expects callbacks from the Checkout to validate an order’s stock status It also has the possibility to update the checkout with an updated ClientOrderNumber.
        /// May contain a {checkout.order.uri} placeholder which will be replaced with the checkoutorderid.
        /// Requests for this endpoint are made with HTTP Method GET. Your response's HTTP Status Code is interpreted as:
        /// 200-299 is interpreted as validation passed.
        /// Everything else is interpreted as validation failure.
        /// </summary>
        /// <remarks>Data type: Url. Max length: 500.</remarks>
        /// </param>
        /// <param name="activePartPaymentCampaigns">
        /// <summary>
        /// List of valid CampaignIDs. If used, a list of available part payment campaign options will be filtered through the chosen list.
        /// </summary>
        /// <remarks>Must be a list of valid CampaignIDs.</remarks>
        /// </param>
        /// <param name="promotedPartPaymentCampaign">
        /// <summary>
        /// If used, the chosen campaign will be listed first in all payment method lists.
        /// </summary>
        /// <remarks>Must be valid CampaignID.</remarks>
        /// </param>
        public MerchantSettings(Uri pushUri, Uri termsUri, Uri checkoutUri, Uri confirmationUri,
            Uri checkoutValidationCallBackUri = null, IList<long> activePartPaymentCampaigns = null,
            long? promotedPartPaymentCampaign = null)
        {
            PushUri = pushUri ?? throw new ArgumentNullException(nameof(pushUri));
            TermsUri = termsUri ?? throw new ArgumentNullException(nameof(termsUri));
            CheckoutUri = checkoutUri ?? throw new ArgumentNullException(nameof(checkoutUri));
            ConfirmationUri = confirmationUri ?? throw new ArgumentNullException(nameof(confirmationUri));
            CheckoutValidationCallBackUri = checkoutValidationCallBackUri;
            ActivePartPaymentCampaigns = activePartPaymentCampaigns;
            PromotedPartPaymentCampaign = promotedPartPaymentCampaign;
        }

        /// <summary>
        /// An optional URI to a location that expects callbacks from the Checkout to validate an order’s stock status It also has the possibility to update the checkout with an updated ClientOrderNumber.
        /// May contain a {checkout.order.uri} placeholder which will be replaced with the checkoutorderid.
        /// Requests for this endpoint are made with HTTP Method GET. Your response's HTTP Status Code is interpreted as:
        /// 200-299 is interpreted as validation passed.
        /// Everything else is interpreted as validation failure.
        /// </summary>
        /// <remarks>Data type: Url. Max length: 500.</remarks>
        public Uri CheckoutValidationCallBackUri { get; }

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
        public Uri PushUri { get; }

        /// <summary>
        /// URI to a page with webshop specific terms.
        /// </summary>
        /// <remarks>Required. Data type: Url. Max length: 500. Min length: 1.</remarks>
        public Uri TermsUri { get; }

        /// <summary>
        /// URI to the page in the webshop displaying the Checkout. May not contain order specific information.
        /// </summary>
        /// <remarks>Required. Data type: Url. Max length: 500. Min length: 1.</remarks>
        public Uri CheckoutUri { get; }

        /// <summary>
        /// URI to the page in the webshop displaying specific information to a customer after the order has been confirmed. May not contain order specific information.
        /// </summary>
        /// <remarks>Required. Data type: Url. Max length: 500. Min length: 1.</remarks>
        public Uri ConfirmationUri { get; }

        /// <summary>
        /// List of valid CampaignIDs. If used, a list of available part payment campaign options will be filtered through the chosen list.
        /// </summary>
        /// <remarks>Must be a list of valid CampaignIDs.</remarks>
        public IList<long> ActivePartPaymentCampaigns { get; }

        /// <summary>
        /// If used, the chosen campaign will be listed first in all payment method lists.
        /// </summary>
        /// <remarks>Must be valid CampaignID.</remarks>
        public long? PromotedPartPaymentCampaign { get; }
    }
}