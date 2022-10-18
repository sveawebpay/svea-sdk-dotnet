namespace Svea.WebPay.SDK.CheckoutApi
{
    public class UpdateOrderModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="merchantData">
        /// <summary>
        /// Metadata visible to the store
        /// </summary>
        /// <remarks>Max length: 6000. Optional. Cleaned up from Checkout database after 45 days.</remarks>
        /// </param>
        /// <param name="shippingInformation"></param>
        public UpdateOrderModel(Cart cart, string merchantData = null, ShippingInformation shippingInformation = null)
        {
            Cart = cart;
            MerchantData = merchantData;
            ShippingInformation = shippingInformation;
        }

        public Cart Cart { get; }

        /// <summary>
        /// Metadata visible to the store
        /// </summary>
        /// <remarks>Max length: 6000. Optional. Cleaned up from Checkout database after 45 days.</remarks>
        public string MerchantData { get; }

        /// <summary>
        /// Shipping information to be updated. Only applicable if merchant has shipping enabled.		
        /// </summary>
        public ShippingInformation ShippingInformation { get; }
    }
}