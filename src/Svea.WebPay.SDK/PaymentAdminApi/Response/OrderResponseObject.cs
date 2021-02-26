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

        /// <summary>
        /// A list of actions possible on the order.
        /// </summary>
        [JsonInclude]
        public IList<string> Actions { get; }

        /// <summary>
        /// Billing address of identified customer.
        /// </summary>
        [JsonInclude]
        public Address BillingAddress { get; }

        /// <summary>
        /// The total cancelled amount of the order. Minor unit.
        /// </summary>
        [JsonInclude]
        public MinorUnit CancelledAmount { get; }

        /// <summary>
        /// Date and time when the order was created.
        /// </summary>
        [JsonInclude]
        public DateTime CreationDate { get; }

        /// <summary>
        /// The current currency as defined by ISO 4217, i.e. SEK, NOK etc.
        /// </summary>
        [JsonInclude]
        public string Currency { get; }

        /// <summary>
        /// Only available on Invoice order.
        /// </summary>
        [JsonInclude]
        public string CustomerReference { get; }

        /// <summary>
        /// List of deliveries.
        /// </summary>
        [JsonInclude]
        public IList<DeliveryResponseObject> Deliveries { get; }

        /// <summary>
        /// The customer’s email address.
        /// </summary>
        [JsonInclude]
        public EmailAddress EmailAddress { get; }

        /// <summary>
        /// Checkout-order ID of the order.
        /// </summary>
        [JsonInclude]
        public long Id { get; }

        /// <summary>
        /// True if national ID is organization number. False if national ID is personal number.
        /// </summary>
        [JsonInclude]
        public bool IsCompany { get; }

        /// <summary>
        /// A string with a maximum of 32 characters that identifies the order in the merchant’s systems.
        /// </summary>
        [JsonInclude]
        public string MerchantOrderId { get; }

        /// <summary>
        /// Personal- or organization number.
        /// </summary>
        [JsonInclude]
        public string NationalId { get; }

        /// <summary>
        /// The total amount of the order. Minor unit.
        /// </summary>
        [JsonInclude]
        public MinorUnit OrderAmount { get; }

        /// <summary>
        /// List of order rows.
        /// </summary>
        [JsonInclude]
        public IList<OrderRowResponseObject> OrderRows { get; }

        /// <summary>
        /// The current state of the order. See list of possible OrderStatus below.
        /// </summary>
        [JsonInclude]
        public OrderStatus OrderStatus { get; }

        /// <summary>
        /// The final payment method for the order. Will only have a value when the order is locked, otherwise null. See list of possible PaymentType below.
        /// </summary>
        [JsonInclude]
        public PaymentType PaymentType { get; }

        /// <summary>
        /// Minimum 6 characters long Peppol Identifier.
        /// </summary>
        [JsonInclude]
        public string PhoneNumber { get; }

        /// <summary>
        /// The customer’s phone number.
        /// </summary>
        [JsonInclude]
        public Address ShippingAddress { get; }

        /// <summary>
        /// Shipping address of identified customer.
        /// </summary>
        [JsonInclude]
        public bool? SveaWillBuy { get; }
    }
}