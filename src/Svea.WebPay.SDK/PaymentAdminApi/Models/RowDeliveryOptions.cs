namespace Svea.WebPay.SDK.PaymentAdminApi.Models
{
    public class RowDeliveryOptions
    {
        /// <summary>
        /// </summary>
        /// <param name="orderRowId">Order row id from underlying system.
        /// <remarks>Id should be present in the OrderRowIds collection in request.</remarks>
        /// </param>
        /// <param name="quantity">Number of items to be delivered for specified row.
        /// <remarks>Quantity should not be below 0 or above the quantity of the row.</remarks>
        /// </param>
        public RowDeliveryOptions(long orderRowId, MinorUnit quantity)
        {
            OrderRowId = orderRowId;
            Quantity = quantity;
        }

        /// <summary>
        /// Order row id from underlying system.
        /// </summary>
        public long OrderRowId { get; }

        /// <summary>
        /// Number of items to be delivered for specified row.
        /// </summary>
        public MinorUnit Quantity { get; }
    }
}
