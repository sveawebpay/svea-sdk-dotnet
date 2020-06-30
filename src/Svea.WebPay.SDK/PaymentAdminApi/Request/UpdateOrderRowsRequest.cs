namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UpdateOrderRowsRequest
    {
        /// <summary>
        /// UpdateOrderRowsRequest
        /// </summary>
        /// <param name="orderRows">To update several order rows with RowId specified.</param>
        public UpdateOrderRowsRequest(List<NewOrderRow> orderRows)
        {
            if (orderRows == null)
            {
                throw new ArgumentNullException(nameof(orderRows));
            }

            OrderRows = orderRows.Select(x => new NewRow(x.OrderRowId ?? 0, x)).ToList();
        }

        public List<NewRow> OrderRows { get; }
    }
}
