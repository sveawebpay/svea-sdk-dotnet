namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    using System;
    using System.Collections.Generic;

    public class CreditNewOrderRowRequest 
    {
        /// <summary>
        /// CreditNewOrderRowRequest
        /// </summary>
        /// <param name="newCreditOrderRow">The new credit row</param>
        /// <param name="newCreditOrderRows">Use this if crediting multiple new rows</param>
        /// 
        public CreditNewOrderRowRequest(CreditOrderRow newCreditOrderRow,  IList<CreditOrderRow> newCreditOrderRows = null) //TimeSpan? pollingTimeout = null)
        {
            NewCreditOrderRow = newCreditOrderRow ?? throw new ArgumentNullException(nameof(newCreditOrderRow));
            NewCreditOrderRows = newCreditOrderRows;
        }

        public CreditOrderRow NewCreditOrderRow { get; }
        public IList<CreditOrderRow> NewCreditOrderRows { get; }
    }
}
