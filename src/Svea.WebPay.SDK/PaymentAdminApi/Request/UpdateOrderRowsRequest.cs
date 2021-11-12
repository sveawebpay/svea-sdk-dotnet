namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;

    public class UpdateOrderRowsRequest : IConfigurableAwait
    {
        /// <summary>
        /// UpdateOrderRowsRequest
        /// </summary>
        /// <param name="orderRows">To update several order rows with RowId specified.</param>
        /// <param name="configureAwait">true to attempt to marshal the continuation back to the original context captured; otherwise, false.</param>
        public UpdateOrderRowsRequest(List<NewOrderRow> orderRows, bool configureAwait = false) 
        {
            if (orderRows == null)
            {
                throw new ArgumentNullException(nameof(orderRows));
            }

            ConfigureAwait = configureAwait;

            OrderRows = orderRows.Select(x => new NewRow(x.OrderRowId ?? 0, x)).ToList();
        }

        public List<NewRow> OrderRows { get; }

        [JsonIgnore]
        public bool ConfigureAwait { get; }
    }
}
