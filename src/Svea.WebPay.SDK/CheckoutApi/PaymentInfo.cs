namespace Svea.WebPay.SDK.CheckoutApi
{
    public class PaymentInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymentMethodType">
        /// <summary>
        /// The final payment method type for the order.
        /// </summary></param>
        /// <param name="campaignCode">
        /// <summary>
        /// Campaign code
        /// </summary>
        /// <remarks>Only available for PaymentPlan</remarks>
        /// </param>
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
