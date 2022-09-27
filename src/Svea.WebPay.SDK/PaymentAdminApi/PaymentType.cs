namespace Svea.WebPay.SDK.PaymentAdminApi
{
    public enum PaymentType
    {
        Unknown = default,

        /// <summary>
        /// Invoice order
        /// </summary>
        Invoice,

        /// <summary>
        /// PaymentPlan order
        /// </summary>
        PaymentPlan,

        /// <summary>
        /// AccountCredit order
        /// </summary>
        AccountCredit,

        /// <summary>
        /// Card order
        /// </summary>
        Card,

        /// <summary>
        /// DirectBank order
        /// </summary>
        DirectBank, 

        /// <summary>
        /// Swish order
        /// </summary>
        Swish,

        /// <summary>
        /// Vipps order
        /// </summary>
        Vipps,

        Leasing,

        MobilePay
    }
}
