namespace Svea.WebPay.SDK.CheckoutApi
{
    /// <summary>
    /// The final payment method type for the order
    /// </summary>
    public enum PaymentMethodType
    {
        Unknown = default,
        AccountCredit,
        Card,
        DirectBank,
        Invoice,
        Leasing,
        PaymentPlan,
        Trustly,
        Swish,
        Vipps,
        MobilePay
    }
}
