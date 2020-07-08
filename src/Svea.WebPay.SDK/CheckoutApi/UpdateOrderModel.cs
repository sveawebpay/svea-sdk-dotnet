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
        public UpdateOrderModel(Cart cart, string merchantData = null)
        {
            Cart = cart;
            MerchantData = merchantData;
        }

        public Cart Cart { get; }

        /// <summary>
        /// Metadata visible to the store
        /// </summary>
        /// <remarks>Max length: 6000. Optional. Cleaned up from Checkout database after 45 days.</remarks>
        private string MerchantData { get; }
    }
}