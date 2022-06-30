﻿namespace Svea.WebPay.SDK.CheckoutApi
{
    public class ShippingOption : IShippingOption
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id of the carrier, nShift specific (typically in form of a guid)</param>
        /// <param name="orderId">Id of the order</param>
        /// <param name="carrier">Name of the carrier, nShift specific</param>
        /// <param name="name">Delivery option name, nShift specific</param>
        /// <param name="price">Price of the parcel, NOT minor currency!</param>
        public ShippingOption(string id, long orderId, string carrier, string name, long price, string description, string postalCode, string timeslot)
        {
            Id = id;
            OrderId = orderId;
            Carrier = carrier;
            Name = name;
            Price = price;
            Description = description;
            PostalCode = postalCode;
            Timeslot = timeslot;
        }

        /// <summary>
        /// Id of the carrier, nShift specific (typically in form of a guid)	
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Id of the order
        /// </summary>
        public long OrderId { get; }

        /// <summary>
        /// Name of the carrier, nShift specific
        /// </summary>
        public string Carrier { get; }

        /// <summary>
        /// Delivery option name, nShift specific
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Price of the parcel, NOT minor currency!
        /// </summary>
        public long Price { get; }

        public string Description { get; }
        public string PostalCode { get; }
        public string Timeslot { get; }
    }
}