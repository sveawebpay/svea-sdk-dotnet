namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    using System;
    using System.Collections.Generic;

    public class CreditNewOrderRowRequest : IResourceRequest
    {
        /// <summary>
        /// CreditNewOrderRowRequest
        /// </summary>
        /// <param name="newCreditOrderRow">The new credit row</param>
        /// <param name="newCreditOrderRows">Use this if crediting multiple new rows</param>
        /// <param name="pollingTimeout">If set the task will be polled until the resource is complete or the timeout has passed. If null the resource will be returned if complete, otherwise the task is returned. </param>
        public CreditNewOrderRowRequest(CreditOrderRow newCreditOrderRow,  IList<CreditOrderRow> newCreditOrderRows = null, TimeSpan? pollingTimeout = null)
        {
            NewCreditOrderRow = newCreditOrderRow ?? throw new ArgumentNullException(nameof(newCreditOrderRow));
            NewCreditOrderRows = newCreditOrderRows;
            PollingTimeout = pollingTimeout;
        }

        public CreditOrderRow NewCreditOrderRow { get; }
        public IList<CreditOrderRow> NewCreditOrderRows { get; }
        public TimeSpan? PollingTimeout { get; }
    }
}
