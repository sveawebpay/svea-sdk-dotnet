namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{

    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class DeliveryResponseObject
    {
        [JsonConstructor]
        public DeliveryResponseObject(IList<string> actions, DateTime creationDate, MinorUnit creditedAmount, IList<Credit> credits,
            MinorUnit deliveryAmount, DateTime? dueDate, long id, long? invoiceId, IList<OrderRowResponseObject> orderRows, string status)
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

        [JsonInclude]
        public IList<string> Actions { get; }

        [JsonInclude]
        public DateTime CreationDate { get; }

        [JsonInclude]
        public MinorUnit CreditedAmount { get; }

        [JsonInclude]
        public IList<Credit> Credits { get; }

        [JsonInclude]
        public MinorUnit DeliveryAmount { get; }

        [JsonInclude]
        public DateTime? DueDate { get; }

        [JsonInclude]
        public long Id { get; }

        [JsonInclude]
        public long? InvoiceId { get; }

        [JsonInclude]
        public IList<OrderRowResponseObject> OrderRows { get; }

        [JsonInclude]
        public string Status { get; }
    }
}