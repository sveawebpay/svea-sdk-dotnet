namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class CreditNewOrderRowRequest : IConfigurableAwait
    {
        /// <summary>
        /// CreditNewOrderRowRequest
        /// </summary>
        /// <param name="newCreditOrderRow">The new credit row</param>
        /// <param name="newCreditOrderRows">Use this if crediting multiple new rows</param>
        /// <param name="configureAwait">true to attempt to marshal the continuation back to the original context captured; otherwise, false.</param>
        public CreditNewOrderRowRequest(CreditOrderRow newCreditOrderRow,  IList<CreditOrderRow> newCreditOrderRows = null, bool configureAwait = false) 
        {
            NewCreditOrderRow = newCreditOrderRow ?? throw new ArgumentNullException(nameof(newCreditOrderRow));
            NewCreditOrderRows = newCreditOrderRows;
            ConfigureAwait = configureAwait;
        }

        public CreditOrderRow NewCreditOrderRow { get; }
        public IList<CreditOrderRow> NewCreditOrderRows { get; }

        [JsonIgnore]
        public bool ConfigureAwait { get; }
    }
}
