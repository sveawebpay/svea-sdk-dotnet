namespace Svea.WebPay.SDK.PaymentAdminApi.Models
{
    using Svea.WebPay.SDK.PaymentAdminApi.Response;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Order
    {
        public Order(OrderResponseObject orderResponseObject, SveaHttpClient client)
        {
            BillingAddress = orderResponseObject.BillingAddress;
            CancelledAmount = orderResponseObject.CancelledAmount;
            CreationDate = orderResponseObject.CreationDate;
            Currency = orderResponseObject.Currency;
            CustomerReference = orderResponseObject.CustomerReference;
            EmailAddress = orderResponseObject.EmailAddress;
            Id = orderResponseObject.Id;
            IsCompany = orderResponseObject.IsCompany;
            MerchantOrderId = orderResponseObject.MerchantOrderId;
            NationalId = orderResponseObject.NationalId;
            OrderAmount = orderResponseObject.OrderAmount;
            OrderStatus = orderResponseObject.OrderStatus;
            PaymentType = orderResponseObject.PaymentType;
            PhoneNumber = orderResponseObject.PhoneNumber;
            ShippingAddress = orderResponseObject.ShippingAddress;
            SveaWillBuy = orderResponseObject.SveaWillBuy;
            AvailableActions = orderResponseObject.Actions;

            Actions = new OrderActions(orderResponseObject, client);
            OrderRows = orderResponseObject.OrderRows?.Select(x => new OrderRow(orderResponseObject.Id, x, client)).ToList();
            Deliveries = orderResponseObject.Deliveries?.Select(x => new Delivery(orderResponseObject.Id, x, client)).ToList();
        }

        public OrderActions Actions { get; }

        public IList<string> AvailableActions { get; }
        public Address BillingAddress { get; }
        public MinorUnit CancelledAmount { get; }
        public DateTime CreationDate { get; }
        public string Currency { get; }
        public string CustomerReference { get; }
        public IList<Delivery> Deliveries { get; }
        public EmailAddress EmailAddress { get; }
        public long Id { get; }
        public bool IsCompany { get; }
        public string MerchantOrderId { get; }
        public string NationalId { get; }
        public MinorUnit OrderAmount { get; }
        public IList<OrderRow> OrderRows { get; }
        public OrderStatus OrderStatus { get; }
        public PaymentType PaymentType { get; }
        public string PhoneNumber { get; }
        public Address ShippingAddress { get; }
        public bool? SveaWillBuy { get; }
    }
}
