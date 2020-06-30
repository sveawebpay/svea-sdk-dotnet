namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using Newtonsoft.Json;

    using System;
    using System.Collections.Generic;

    public class OrderResponseObject
    {
        public OrderResponseObject(){}

        [JsonConstructor]
        internal OrderResponseObject(IList<string> actions, Address billingAddress, MinorUnit cancelledAmount, DateTime creationDate,
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

        internal IList<string> Actions { get; }
        internal Address BillingAddress { get; }
        internal MinorUnit CancelledAmount { get; }
        internal DateTime CreationDate { get; }
        internal string Currency { get; }
        internal string CustomerReference { get; }
        internal IList<DeliveryResponseObject> Deliveries { get; }
        internal EmailAddress EmailAddress { get; }
        internal long Id { get; }
        internal bool IsCompany { get; }
        internal string MerchantOrderId { get; }
        internal string NationalId { get; }
        internal MinorUnit OrderAmount { get; }
        internal IList<OrderRowResponseObject> OrderRows { get; }
        internal OrderStatus OrderStatus { get; }
        internal PaymentType PaymentType { get; }
        internal string PhoneNumber { get; }
        internal Address ShippingAddress { get; }
        internal bool? SveaWillBuy { get; }
    }
}