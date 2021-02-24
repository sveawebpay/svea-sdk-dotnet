namespace Svea.WebPay.SDK.PaymentAdminApi.Models
{
    using Svea.WebPay.SDK.PaymentAdminApi.Response;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Delivery
    {
        public Delivery(long orderId, DeliveryResponseObject deliveryResponse, SveaHttpClient client)
        {
            Actions = new DeliveryActions(orderId, deliveryResponse, client);

            CreationDate = deliveryResponse.CreationDate;
            CreditedAmount = deliveryResponse.CreditedAmount;
            Credits = deliveryResponse.Credits;
            DeliveryAmount = deliveryResponse.DeliveryAmount;
            DueDate = deliveryResponse.DueDate;
            Id = deliveryResponse.Id;
            InvoiceId = deliveryResponse.InvoiceId;
            Status = deliveryResponse.Status;
            AvailableActions = deliveryResponse.Actions;

            OrderRows = deliveryResponse.OrderRows?.Select(x => new OrderRow(orderId, x, client)).ToList();
        }

        public IList<string> AvailableActions { get; }

        public DeliveryActions Actions { get; }
        public DateTime CreationDate { get; }
        public MinorUnit CreditedAmount { get; }
        public IList<Credit> Credits { get; }
        public MinorUnit DeliveryAmount { get; }
        public DateTime? DueDate { get; }
        public long Id { get; }
        public long? InvoiceId { get; }
        public IList<OrderRow> OrderRows { get; }
        public string Status { get; }
    }
}