namespace Svea.WebPay.SDK.CheckoutApi
{
    public class FallbackOption : IShippingOption
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id of the carrier, nShift specific (typically in form of a guid)</param>
        /// <param name="carrier">Name of the carrier, nShift specific</param>
        /// <param name="name">Delivery option name, nShift specific</param>
        /// <param name="price">Price of the parcel, NOT minor currency!</param>
        public FallbackOption(string id, string carrier, string name, long price)
        {
            Id = id;
            Carrier = carrier;
            Name = name;
            Price = price;
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
        /// Price of the parcel, NOT minor currency!
        /// </summary>
        public long Price { get; }

        public object Addons { get; } //TODO: Add this
        public object Fields { get; }  //TODO: Add this

    }
}