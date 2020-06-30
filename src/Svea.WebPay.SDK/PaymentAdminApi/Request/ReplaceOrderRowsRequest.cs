namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    using System;
    using System.Collections.Generic;

    public class ReplaceOrderRowsRequest
    {
        /// <summary>
        /// ReplaceOrderRowsRequest </summary>
        /// <param name="orderRows">To update several order rows with RowId specified.</param>
        public ReplaceOrderRowsRequest(IList<NewOrderRow> orderRows)
        {
            OrderRows = orderRows ?? throw new ArgumentNullException(nameof(orderRows));
        }

        /// <summary>
        /// To add multiple order rows.
        /// </summary>
        public IList<NewOrderRow> OrderRows { get; }
    }
}
