namespace Svea.WebPay.SDK.CheckoutApi
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class FallbackOption : IShippingOption
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id of the carrier, nShift specific (typically in form of a guid)</param>
        /// <param name="carrier">Name of the carrier, nShift specific</param>
        /// <param name="name">Delivery option name, nShift specific</param>
        /// <param name="shippingFee">Price of the parcel. Minor currency!</param>
        /// <param name="addons">nshift format Addons. As smiliar as nshift models</param>
        /// <param name="fields">nshift format Fields. As smiliar as nshift models</param>
        [JsonConstructor]
        public FallbackOption(string id, string carrier, string name, MinorUnit shippingFee, List<Addon> addons, List<Field> fields)
        {
            Id = id;
            Carrier = carrier;
            Name = name;
            ShippingFee = shippingFee;
            Addons = addons;
            Fields = fields;
        }

        /// <summary>
        /// Id of the carrier, nShift specific (typically in form of a guid)	
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Name of the carrier, nShift specific
        /// </summary>
        public string Carrier { get; }

        /// <summary>
        /// Delivery option name, nShift specific
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Price of the parcel. Minor currency!
        /// </summary>
        public MinorUnit ShippingFee { get; }

        /// <summary>
        /// nshift format Addons. As smiliar as nshift models
        /// </summary>
        public List<Addon> Addons { get; }

        /// <summary>
        /// nshift format Fields. As smiliar as nshift models
        /// </summary>
        public List<Field> Fields { get; }
    }
}