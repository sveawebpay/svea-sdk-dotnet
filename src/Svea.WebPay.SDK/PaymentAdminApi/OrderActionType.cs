namespace Svea.WebPay.SDK.PaymentAdminApi
{
    public static class OrderActionType
    {
        public const string CanCancelOrder = "CanCancelOrder";
        public const string CanCancelOrderRow = "CanCancelOrderRow";
        public const string CanCancelAmount = "CanCancelAmount";

        public const string CanDeliverOrder = "CanDeliverOrder";
        public const string CanDeliverOrderPartially = "CanDeliverPartially";

        public const string CanAddOrderRow = "CanAddOrderRow";
        public const string CanUpdateOrderRow = "CanUpdateOrderRow";
    }
}
