namespace Svea.WebPay.SDK.CheckoutApi
{
    public class ShippingProvider
    {
        public ShippingProvider(string name, string shipmentId, ShippingOption shippingOption)
        {
            Name = name;
            ShipmentId = shipmentId;
            ShippingOption = shippingOption;
        }

        /// <summary>
        /// Name of the shipping provider ('nShift' in this case). If shipping provider (nShift) is down and a fallback option is used then the value will be 'fallback'
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Internal order Id, Id is only set if the order is final, otherwise empty string will be set.
        /// </summary>
        public string ShipmentId { get; }

        /// <summary>
        /// Shipping option set on the order	
        /// </summary>
        public ShippingOption ShippingOption { get; }
    }
}