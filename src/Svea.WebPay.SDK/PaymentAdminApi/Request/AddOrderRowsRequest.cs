﻿namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    using System;
    using System.Collections.Generic;

    public class AddOrderRowsRequest : IConfigurableAwait
    {
        /// <summary>
        /// AddOrderRowsRequest </summary>
        /// <param name="orderRows">To update several order rows with RowId specified.</param>
        /// <param name="configureAwait">true to attempt to marshal the continuation back to the original context captured; otherwise, false.</param>
        public AddOrderRowsRequest(IList<NewOrderRow> orderRows, bool configureAwait = false)
        {
            OrderRows = orderRows ?? throw new ArgumentNullException(nameof(orderRows));
            ConfigureAwait = configureAwait;
        }

        /// <summary>
        /// To add multiple order rows.
        /// </summary>
        public IList<NewOrderRow> OrderRows { get; }

        public bool ConfigureAwait { get; }
    }
}
