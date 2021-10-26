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

        /// <summary>
        /// A list of actions possible on the delivery.
        /// </summary>
        [JsonInclude]
        public IList<string> Actions { get; }

        /// <summary>
        /// Date and time when the order was created.
        /// </summary>
        [JsonInclude]
        public DateTime CreationDate { get; }
        
        /// <summary>
        /// The total credited amount of the delivery. Minor unit.
        /// </summary>
        [JsonInclude]
        public MinorUnit CreditedAmount { get; }

        /// <summary>
        /// List of credit rows.
        /// </summary>
        [JsonInclude]
        public IList<Credit> Credits { get; }

        /// <summary>
        /// The total amount of the delivery. Minor unit.
        /// </summary>
        [JsonInclude]
        public MinorUnit DeliveryAmount { get; }

        /// <summary>
        /// Due date for the payment.
        /// </summary>
        [JsonInclude]
        public DateTime? DueDate { get; }

        /// <summary>
        /// Delivery ID.
        /// </summary>
        [JsonInclude]
        public long Id { get; }
        
        /// <summary>
        /// Invoice identification number. Only set if the payment method is invoice.
        /// </summary>
        [JsonInclude]
        public long? InvoiceId { get; }

        /// <summary>
        /// List of order rows.
        /// </summary>
        [JsonInclude]
        public IList<OrderRowResponseObject> OrderRows { get; }

        /// <summary>
        /// Payment status for the delivery.
        /// </summary>
        [JsonInclude]
        public string Status { get; }
    }
}