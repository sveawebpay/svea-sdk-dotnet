namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{

    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class OrderResponseObject
    {
        public OrderResponseObject(){}

        [JsonConstructor]
        public OrderResponseObject(IList<string> actions, Address billingAddress, MinorUnit cancelledAmount, DateTime creationDate,
            string currency, string customerReference, IList<DeliveryResponseObject> deliveries, EmailAddress emailAddress, long id,
            bool isCompany, string merchantOrderId, string nationalId, MinorUnit orderAmount, IList<OrderRowResponseObject> orderRows,
            OrderStatus orderStatus, PaymentType paymentType, string phoneNumber, Address shippingAddress, bool? sveaWillBuy)
        {
            Actions = actions;
            BillingAddress = billingAddress;
            CancelledAmount = cancelledAmount;
            CreationDate = creationDate;
            Currency = currency;
            CustomerReference = customerReference;
            Deliveries = deliveries;
            EmailAddress = emailAddress;
            Id = id;
            IsCompany = isCompany;
            MerchantOrderId = merchantOrderId;
            NationalId = nationalId;
            OrderAmount = orderAmount;
            OrderRows = orderRows;
            OrderStatus = orderStatus;
            PaymentType = paymentType;
            PhoneNumber = phoneNumber;
            ShippingAddress = shippingAddress;
            SveaWillBuy = sveaWillBuy;
        }

        [JsonInclude]
        public IList<string> Actions { get; }

        [JsonInclude]
        public Address BillingAddress { get; }

        [JsonInclude]
        public MinorUnit CancelledAmount { get; }

        [JsonInclude]
        public DateTime CreationDate { get; }

        [JsonInclude]
        public string Currency { get; }

        [JsonInclude]
        public string CustomerReference { get; }
        
        [JsonInclude]
        public IList<DeliveryResponseObject> Deliveries { get; }

        [JsonInclude]
        public EmailAddress EmailAddress { get; }

        [JsonInclude]
        public long Id { get; }

        [JsonInclude]
        public bool IsCompany { get; }

        [JsonInclude]
        public string MerchantOrderId { get; }

        [JsonInclude]
        public string NationalId { get; }

        [JsonInclude]
        public MinorUnit OrderAmount { get; }

        [JsonInclude]
        public IList<OrderRowResponseObject> OrderRows { get; }

        [JsonInclude]
        public OrderStatus OrderStatus { get; }

        [JsonInclude]
        public PaymentType PaymentType { get; }

        [JsonInclude]
        public string PhoneNumber { get; }

        [JsonInclude]
        public Address ShippingAddress { get; }

        [JsonInclude]
        public bool? SveaWillBuy { get; }
    }
}