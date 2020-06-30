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

        public async Task<Data> CreateOrder(CreateOrderModel createOrderModel)
        {
            var url = new Uri("/api/orders", UriKind.Relative);
            var data = await _sveaHttpClient.HttpPost<Data>(url, createOrderModel);
            return data;
        }


        public async Task<Data> GetOrder(long orderId)
        {
            var url = new Uri($"/api/orders/{orderId}", UriKind.Relative);
            var data = await _sveaHttpClient.HttpGet<Data>(url);
            return data;
        }

        public async Task<Data> UpdateOrder(long orderId, UpdateOrderModel updateOrderModel)
        {
            var url = new Uri($"/api/orders/{orderId}", UriKind.Relative);
            var data = await _sveaHttpClient.HttpPut<Data>(url, updateOrderModel);
            return data;
        }
    }
}