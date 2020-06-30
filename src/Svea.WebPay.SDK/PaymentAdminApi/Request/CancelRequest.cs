namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    public class CancelRequest
    {
        /// <summary>
        /// CancelRequest
        /// </summary>
        /// <param name="isCancelled">Set to true to cancel order. This cannot be undone</param>
        public CancelRequest(bool isCancelled)
        {
            IsCancelled = isCancelled;
        }

        public bool IsCancelled { get; }
    }
}
