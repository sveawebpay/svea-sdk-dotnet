namespace Svea.WebPay.SDK.PaymentAdminApi
{
    public enum OrderStatus
    {
        Unknown = default,

        /// <summary>
        /// The order is open and active. This includes partially delivered orders
        /// </summary>
        Open,

        /// <summary>
        /// The order is fully delivered
        /// </summary>
        Delivered, 

        /// <summary>
        /// The order is fully cancelled
        /// </summary>
        Cancelled, 

        /// <summary>
        /// The payment for this order has failed
        /// </summary>
        Failed,

        /// <summary>
        /// The order does not have a set Payment Method
        /// </summary>
        Processing, 
    }
}
