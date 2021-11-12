namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class CreditOrderRowWithFeeRequest : IConfigurableAwait
    {
        /// <summary>
        /// CreditOrderRowsRequest
        /// </summary>
        /// <param name="orderRowIds">ID of the delivered order rows that will be credited.</param>
        /// <param name="fee">An object containing details of fee on the credit. This is an optional field.See Fee structure in Data objects chapter.</param>
        /// <param name="rowCreditingOptions">An object containing details of partial crediting of order rows. This is an optional field.See RowCreditingOptions structure in Data objects chapter. This parameter can be used only to partially credit Invoice orders.</param>
        /// <param name="configureAwait">true to attempt to marshal the continuation back to the original context captured; otherwise, false.</param>
        public CreditOrderRowWithFeeRequest(IList<long> orderRowIds, Fee fee = null, IList<RowCreditingOptions> rowCreditingOptions = null, bool configureAwait = false) 
        {
            OrderRowIds = orderRowIds ?? throw new ArgumentNullException(nameof(orderRowIds));
            Fee = fee;
            RowCreditingOptions = rowCreditingOptions;
            ConfigureAwait = configureAwait;
        }

        public IList<long> OrderRowIds { get; }
        public Fee Fee { get; }
        public IList<RowCreditingOptions> RowCreditingOptions { get; }

        [JsonIgnore]
        public bool ConfigureAwait { get; }
    } 
}
