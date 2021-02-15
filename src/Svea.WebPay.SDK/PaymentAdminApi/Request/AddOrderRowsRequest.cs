namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    using System;
    using System.Collections.Generic;

    public class AddOrderRowsRequest : IResourceRequest
    {
        /// <summary>
        /// AddOrderRowsRequest </summary>
        /// <param name="orderRows">To update several order rows with RowId specified.</param>
        /// <param name="pollingTimeout">If set the task will be polled until the resource is complete or the timeout has passed. If null the resource will be returned if complete, otherwise the task is returned. </param>
        public AddOrderRowsRequest(IList<NewOrderRow> orderRows, TimeSpan? pollingTimeout = null)
        {
            OrderRows = orderRows ?? throw new ArgumentNullException(nameof(orderRows));
            PollingTimeout = pollingTimeout;
        }

        /// <summary>
        /// To add multiple order rows.
        /// </summary>
        public IList<NewOrderRow> OrderRows { get; }

        public TimeSpan? PollingTimeout { get; }
    }
}
