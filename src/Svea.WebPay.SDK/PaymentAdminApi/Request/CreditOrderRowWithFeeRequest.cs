namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    using System;
    using System.Collections.Generic;

    public class CreditOrderRowWithFeeRequest
    {
        /// <summary>
        /// CreditOrderRowsRequest
        /// </summary>
        /// <param name="orderRowIds">ID of the delivered order rows that will be credited.</param>
        /// <param name="fee">An object containing details of fee on the credit. This is an optional field.See Fee structure in Data objects chapter.</param>
        /// <param name="rowCreditingOptions">An object containing details of partial crediting of order rows. This is an optional field.See RowCreditingOptions structure in Data objects chapter. This parameter can be used only to partially credit Invoice orders.</param>
        public CreditOrderRowWithFeeRequest(IList<long> orderRowIds, Fee fee = null, IList<RowCreditingOptions> rowCreditingOptions = null)
        {
            OrderRowIds = orderRowIds ?? throw new ArgumentNullException(nameof(orderRowIds));
            Fee = fee;
            RowCreditingOptions = rowCreditingOptions;
        }

        public IList<long> OrderRowIds { get; }
        public Fee Fee { get; }
        public IList<RowCreditingOptions> RowCreditingOptions { get; }
    } 
}
