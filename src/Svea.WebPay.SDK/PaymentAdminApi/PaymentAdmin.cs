using System;
using System.Threading.Tasks;

namespace Svea.WebPay.SDK.PaymentAdminApi
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;
    using Svea.WebPay.SDK.PaymentAdminApi.Response;

    public class PaymentAdmin
    {
        private readonly SveaHttpClient _sveaHttpClient;

        public PaymentAdmin(SveaHttpClient sveaHttpClient)
        {
            _sveaHttpClient = sveaHttpClient;
        }

        /// <summary>
        /// This method is used to get the entire order with all its relevant information. Including its
        /// deliveries, rows, credits and addresses.
        /// </summary>
        /// <param name="orderId">Checkout order id of the specified order.</param>
        /// <returns></returns>
        public async Task<Order> GetOrder(long orderId)
        {
            var url = new Uri($"/api/v1/orders/{orderId}", UriKind.Relative);
            var orderResponse = await _sveaHttpClient.HttpGet<OrderResponseObject>(url);
            var order = new Order(orderResponse, _sveaHttpClient);

            
            return order;
        }

        /// <summary>
        /// This method is used to get the entire order with all its relevant information. Including its
        /// deliveries, rows, credits and addresses.
        /// </summary>
        /// <param name="orderUri">Uri of the specified order.</param>
        /// <returns></returns>
        public async Task<Order> GetOrder(Uri orderUri)
        {
            var orderResponse = await _sveaHttpClient.HttpGet<OrderResponseObject>(orderUri);
            var order = new Order(orderResponse, _sveaHttpClient);

            return order;
        }

        /// <summary>
        /// A task will explain the status of a previously performed operation. When finished it will point
        /// towards the new resource with the Location header.
        /// </summary>
        /// <param name="taskId">Id of the queued task</param>
        /// <returns></returns>
        public async Task<Task> GetTask(long taskId)
        {
            var url = new Uri($"/api/v1/queue/{taskId}", UriKind.Relative);
            var task = await _sveaHttpClient.HttpGet<Task>(url);
            
            return task;
        }

        /// <summary>
        /// A task will explain the status of a previously performed operation. When finished it will point
        /// towards the new resource with the Location header.
        /// </summary>
        /// <param name="taskUri">Uri to the queued task</param>
        /// <returns></returns>
        public async Task<Task> GetTask(Uri taskUri)
        {
            var task = await _sveaHttpClient.HttpGet<Task>(taskUri);
            return task;
        }
    }
}
