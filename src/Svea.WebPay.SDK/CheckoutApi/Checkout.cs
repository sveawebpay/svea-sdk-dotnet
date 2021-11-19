using System;
using System.Threading.Tasks;

namespace Svea.WebPay.SDK.CheckoutApi
{
    public class Checkout
    {
        private readonly SveaHttpClient _sveaHttpClient;

        public Checkout(SveaHttpClient sveaHttpClient)
        {
            _sveaHttpClient = sveaHttpClient;
        }

        /// <summary>
        /// This request creates a new order and returns the checkout to the webshop.
        /// You can add preset values to the call, as well.These values will prefill the identification in the checkout.If a preset value has IsReadOnly, the customer will not be able to modify the value.
        /// </summary>
        /// <param name="createOrderModel"></param>
        /// <param name="configureAwait">true to attempt to marshal the continuation back to the original context captured; otherwise, false.</param>
        /// <returns></returns>
        public async Task<Data> CreateOrder(CreateOrderModel createOrderModel, bool configureAwait = false)
        {
            var url = new Uri("/api/orders", UriKind.Relative);
            var data = await _sveaHttpClient.HttpPost<Data>(url, createOrderModel, configureAwait);
            return data;
        }

        /// <summary>
        /// This request returns a data structure that contains all information about the order and what is needed for the GUI.
        /// </summary>
        /// <param name="orderId">Checkout orderId of the specified order.</param>
        /// <param name="configureAwait">true to attempt to marshal the continuation back to the original context captured; otherwise, false.</param>
        /// <returns></returns>
        public async Task<Data> GetOrder(long orderId, bool configureAwait = false)
        {
            var url = new Uri($"/api/orders/{orderId}", UriKind.Relative);
            var data = await _sveaHttpClient.HttpGet<Data>(url, configureAwait);
            return data;
        }

        /// <summary>
        /// This request replaces the order rows of the specified order with the new appended in the call and sets the MerchantData on the order to the provided value.
        /// </summary>
        /// <param name="orderId">Checkoutorderid of the specified order.</param>
        /// <param name="updateOrderModel">Contains the order rows that will be set to the specified order, as well as MerchantData.</param>
        /// <param name="configureAwait">true to attempt to marshal the continuation back to the original context captured; otherwise, false.</param>
        /// <remarks>Quantity, UnitPrice, Discount and VatPercent for each order row is expected to be given in minor currency.</remarks>
        /// <returns></returns>
        public async Task<Data> UpdateOrder(long orderId, UpdateOrderModel updateOrderModel, bool configureAwait = false)
        {
            var url = new Uri($"/api/orders/{orderId}", UriKind.Relative);
            var data = await _sveaHttpClient.HttpPut<Data>(url, updateOrderModel, configureAwait);
            return data;
        }
    }
}