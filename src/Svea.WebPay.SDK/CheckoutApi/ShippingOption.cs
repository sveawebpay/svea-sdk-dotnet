namespace Svea.WebPay.SDK.CheckoutApi
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class ShippingOption : IShippingOption
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id of the carrier, nShift specific (typically in form of a guid)</param>
        /// <param name="orderId">Id of the order</param>
        /// <param name="carrier">Name of the carrier, nShift specific</param>
        /// <param name="name">Delivery option name, nShift specific</param>
        /// <param name="price">OBSOLETE (Please use 'shippingFee' property instead!) Price of the parcel, NOT minor currency!</param>
        /// <param name="description"></param>
        /// <param name="postalCode"></param>
        /// <param name="timeslot"></param>
        /// <param name="shippingFee">Price of the parcel. Minor currency!</param>
        /// <param name="totalShippingFee"></param>
        /// <param name="location"></param>
        /// <param name="fields">nshift format Fields. As smiliar as nshift models.</param>
        /// <param name="addons">nshift format Addons. As smiliar as nshift models.</param>
        [JsonConstructor]
        public ShippingOption(
            string id, 
            long orderId, 
            string carrier, 
            string name, 
            long price, 
            string description, 
            string postalCode, 
            string timeslot,
            long shippingFee,
            long totalShippingFee,
            Location location,
            List<Field> fields, 
            List<Addon> addons)
        {
            Id = id;
            OrderId = orderId;
            Carrier = carrier;
            Name = name;
            Price = price;
            Description = description;
            PostalCode = postalCode;
            Timeslot = timeslot;
            ShippingFee = shippingFee;
            TotalShippingFee = totalShippingFee;
            Location = location;
            Fields = fields;
            Addons = addons;
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
        [Obsolete("OBSOLETE (Please use 'shippingFee' property instead!)")]
        public long Price { get; }

        /// <summary>
        /// Price of the parcel. Minor currency!
        /// </summary>
        public long ShippingFee { get; }

        public long TotalShippingFee { get; }
        public Location Location { get; }

        /// <summary>
        /// nshift format Fields. As smiliar as nshift models.
        /// </summary>
        public List<Field> Fields { get; }

        /// <summary>
        /// nshift format Addons. As smiliar as nshift models
        /// </summary>
        public List<Addon> Addons { get; }

        public string Description { get; }
        public string PostalCode { get; }
        public string Timeslot { get; }
    }
}