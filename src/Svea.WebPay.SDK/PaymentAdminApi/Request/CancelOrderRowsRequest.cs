namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    public class CancelOrderRowsRequest
    {
        /// <summary>
        /// CancelOrderRowsRequest
        /// </summary>
        /// <param name="orderRowIds">Id of the rows that will be cancelled.</param>
        public CancelOrderRowsRequest(long[] orderRowIds)
        {
            OrderRowIds = orderRowIds;
        }

        public long[] OrderRowIds { get; }
    }
}
