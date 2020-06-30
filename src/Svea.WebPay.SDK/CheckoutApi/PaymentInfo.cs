namespace Svea.WebPay.SDK.CheckoutApi
{
    public class PaymentInfo
    {
        public PaymentInfo(PaymentMethodType? paymentMethodType, long campaignCode)
        {
            PaymentMethodType = paymentMethodType;
            CampaignCode = campaignCode;
        }

        /// <summary>
        /// The final payment method type for the order.
        /// </summary>
        public PaymentMethodType? PaymentMethodType { get; set; }

        /// <summary>
        /// Campaign code
        /// </summary>
        /// <remarks>Only available for PaymentPlan</remarks>
        public long CampaignCode { get; set; }
    }
}
