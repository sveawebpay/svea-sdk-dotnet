namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using Newtonsoft.Json;

    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    using System;
    using System.Collections.Generic;

    public class DeliveryResponseObject
    {
        [JsonConstructor]
        internal DeliveryResponseObject(IList<string> actions, DateTime creationDate, long creditedAmount, IList<Credit> credits,
            long deliveryAmount, DateTime? dueDate, long id, long? invoiceId, IList<OrderRowResponseObject> orderRows, string status)
        {
            Actions = actions;
            CreationDate = creationDate;
            CreditedAmount = creditedAmount;
            Credits = credits;
            DeliveryAmount = deliveryAmount;
            DueDate = dueDate;
            Id = id;
            InvoiceId = invoiceId;
            OrderRows = orderRows;
            Status = status;
        }

        internal IList<string> Actions { get; }
        internal DateTime CreationDate { get; }
        internal long CreditedAmount { get; }
        internal IList<Credit> Credits { get; }
        internal long DeliveryAmount { get; }
        internal DateTime? DueDate { get; }
        internal long Id { get; }
        internal long? InvoiceId { get; }
        internal IList<OrderRowResponseObject> OrderRows { get; }
        internal string Status { get; }
    }
}