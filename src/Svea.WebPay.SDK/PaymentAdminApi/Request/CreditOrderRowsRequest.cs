namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using System;
    using System.Collections.Generic;

    public class CreditOrderRowsRequest : IResourceRequest
    {
        /// <summary>
        /// CreditOrderRowsRequest
        /// </summary>
        /// <param name="orderRowIds">ID of the delivered order rows that will be credited.</param>
        /// <param name="pollingTimeout">If set the task will be polled until the resource is complete or the timeout has passed. If null the resource will be returned if complete, otherwise the task is returned. </param>
        public CreditOrderRowsRequest(IList<long> orderRowIds, TimeSpan? pollingTimeout = null)
        {
            OrderRowIds = orderRowIds ?? throw new ArgumentNullException(nameof(orderRowIds));
            PollingTimeout = pollingTimeout;
        }

        public IList<long> OrderRowIds { get; }
        public TimeSpan? PollingTimeout { get; }
    } 
}
