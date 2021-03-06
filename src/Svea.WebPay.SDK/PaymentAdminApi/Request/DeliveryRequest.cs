﻿namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    using System;
    using System.Collections.Generic;

    public class DeliveryRequest
    {
        /// <summary>
        /// DeliveryRequest
        /// </summary>
        /// <param name="orderRows">ID of the order rows to be delivered</param>
        /// <param name="invoiceDistributionType">The InvoiceDistributionType parameter can only be used for orders made with the payment method invoice. If the payment method is not specified, the default distribution type "Post" will be used.</param>
        /// <param name="rowDeliveryOptions">An object containing details of partial delivery of order rows. This is an optional field. See RowDeliveryOptions structure in Data objects chapter.</param>
        public DeliveryRequest(IList<long> orderRows, InvoiceDistributionType? invoiceDistributionType = null, IList<RowDeliveryOptions> rowDeliveryOptions = null)
        {
            OrderRowIds = orderRows ?? throw new ArgumentNullException(nameof(orderRows));
            InvoiceDistributionType = invoiceDistributionType;
            RowDeliveryOptions = rowDeliveryOptions;
        }

        public IList<long> OrderRowIds { get; }
        public InvoiceDistributionType? InvoiceDistributionType { get; }
        public IList<RowDeliveryOptions> RowDeliveryOptions { get; }
    }
}
