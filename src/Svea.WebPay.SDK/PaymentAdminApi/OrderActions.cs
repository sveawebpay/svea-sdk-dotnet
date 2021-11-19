using System;
using System.Threading.Tasks;

namespace Svea.WebPay.SDK.PaymentAdminApi
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;
    using Svea.WebPay.SDK.PaymentAdminApi.Request;
    using Svea.WebPay.SDK.PaymentAdminApi.Response;

    public class OrderActions
    {
        public OrderActions(OrderResponseObject orderResponse, SveaHttpClient client)
        {
            if (orderResponse.Actions == null)
            {
                return;
            }

            foreach (var action in orderResponse.Actions)
            {
                switch (action)
                {
                    case OrderActionType.CanCancelOrder:
                        Cancel = async (payload) => await client.HttpPatch<object>(
                            new Uri($"/api/v1/orders/{orderResponse.Id}/", 
                                UriKind.Relative), payload, payload.ConfigureAwait).ConfigureAwait(payload.ConfigureAwait);
                        break;
                    case OrderActionType.CanCancelOrderRow:
                        CancelOrderRows = async payload => await client.HttpPatch<object>(
                            new Uri($"/api/v1/orders/{orderResponse.Id}/rows/cancelOrderRows",
                                UriKind.Relative), payload, payload.ConfigureAwait).ConfigureAwait(payload.ConfigureAwait);
                        break;
                    case OrderActionType.CanCancelAmount:
                        CancelAmount = async payload => await client.HttpPatch<object>(
                            new Uri($"/api/v1/orders/{orderResponse.Id}/", 
                                UriKind.Relative), payload, payload.ConfigureAwait).ConfigureAwait(payload.ConfigureAwait);
                        break;
                    case OrderActionType.CanDeliverOrder:
                    case OrderActionType.CanDeliverOrderPartially:
                        DeliverOrder = async (payload, pollingTimeout) =>
                        {
                            var response = await client.HttpPost<ResourceResponseObject<OrderResponseObject>, OrderResponseObject>(
                                new Uri($"/api/v1/orders/{orderResponse.Id}/deliveries", 
                                    UriKind.Relative), payload, pollingTimeout, payload.ConfigureAwait).ConfigureAwait(payload.ConfigureAwait);

                            var resource = new ResourceResponse<OrderResponseObject, Order>(response, () => new Order(response.Resource, client));
                       
                            return resource;
                        };
                        break;
                    case OrderActionType.CanAddOrderRow:
                        AddOrderRow = async (payload,pollingTimeout) =>
                        {
                            var response = await client.HttpPost<ResourceResponseObject<OrderResponseObject>, OrderResponseObject>(
                                new Uri($"/api/v1/orders/{orderResponse.Id}/rows", 
                                    UriKind.Relative), payload, pollingTimeout, payload.ConfigureAwait).ConfigureAwait(payload.ConfigureAwait);

                            var resource = new ResourceResponse<OrderResponseObject, Order>(response, () => new Order(response.Resource, client));

                            return resource;
                        };
                        
                        AddOrderRows = async (payload, pollingTimeout) =>
                        {
                            var response = await client.HttpPost<ResourceResponseObject<OrderResponseObject>, OrderResponseObject>(
                                new Uri($"/api/v1/orders/{orderResponse.Id}/rows/addOrderRows", 
                                    UriKind.Relative), payload, pollingTimeout, payload.ConfigureAwait).ConfigureAwait(payload.ConfigureAwait);

                            var resource = new ResourceResponse<OrderResponseObject, Order>(response, () => new Order(response.Resource, client
                            ));

                            return resource;
                        };


                        break;
                    case OrderActionType.CanUpdateOrderRow:
                        UpdateOrderRows = async payload => await client.HttpPost<object>(
                            new Uri($"/api/v1/orders/{orderResponse.Id}/rows/updateOrderRows",
                                UriKind.Relative), payload, payload.ConfigureAwait).ConfigureAwait(payload.ConfigureAwait);
                        ReplaceOrderRows = async payload => await client.HttpPut<object>(
                            new Uri($"/api/v1/orders/{orderResponse.Id}/rows/replaceOrderRows",
                                UriKind.Relative), payload, payload.ConfigureAwait).ConfigureAwait(payload.ConfigureAwait);
                        break;
                }
            }
        }

        /// <summary>
        /// By setting the IsCancelled parameter to true the order is cancelled, assuming the order has the action “CanCancelOrder”.
        /// </summary>
        public Func<CancelRequest, System.Threading.Tasks.Task> Cancel { get; internal set; }


        /// <summary>
        /// By specifying a higher amount than the current order cancelled amount then the order cancelled
        /// amount will increase, assuming the order has the action “CanCancelOrderAmount”. The delta
        /// between the new CancelledAmount and the former CancelledAmount will be cancelled.
        /// The new CancelledAmount cannot be equal to or lower than the current CancelledAmount or
        /// more than OrderAmount on the Order
        /// </summary>
        public Func<CancelAmountRequest, System.Threading.Tasks.Task> CancelAmount { get; internal set; }

        /// <summary>
        /// Creates a delivery on checkout order {orderId}. Assuming the order got the “CanDeliverOrder” action.
        /// The deliver call should contain a list of all order row ids that should be delivered.
        /// If a complete delivery of all rows should be made the list should either contain all order row ids
        /// or be empty.
        /// However if a subset of all active order rows are specified a partial delivery will be made.Partial
        /// delivery can only be made if the order has the “CanDeliverOrderPartially” action and each
        /// OrderRow must have action "CanDeliverRow".
        /// InvoiceDistributionType parameter can only be used for orders with PaymentType Invoice. If not
        /// specified then default distribution type ‘Post’ will be used.
        /// </summary>
        public Func<DeliveryRequest, PollingTimeout, Task<ResourceResponse<OrderResponseObject, Order>>> DeliverOrder { get; internal set; }

        /// <summary>
        /// This action changes the status of order rows to "Cancelled". This assuming that the order has the action "CanCancelOrderRow"
        /// and the order rows have the action "CanCancelRow".
        /// </summary>
        public Func<CancelOrderRowsRequest, System.Threading.Tasks.Task> CancelOrderRows { get; internal set; }

        /// <summary>
        /// This method is used to add order rows to an order, assuming the order has the action
        /// “CanAddOrderRow”.
        /// If the new order amount will exceed the current order amount, a credit check will be taken.
        /// </summary>
        public Func<AddOrderRowRequest, PollingTimeout, Task<ResourceResponse<OrderResponseObject, Order>>> AddOrderRow { get; internal set; }

        /// <summary>
        /// This method is used to add several order rows to an order, assuming that the order has the action "CanAddOrderRow".
        /// If the new order amount will exceed the current order amount, a credit check will be taken.
        /// </summary>
        public Func<AddOrderRowsRequest, PollingTimeout, Task<ResourceResponse<OrderResponseObject, Order>>> AddOrderRows { get; internal set; }

        /// <summary>
        /// This method is used to update several order rows, assuming that the order has the action "CanUpdateOrderRow", and the order rows have the action "CanUpdateRow".
        /// If the new order amount exceeds the current order amount, a credit check will be made.
        /// </summary>
        public Func<UpdateOrderRowsRequest, System.Threading.Tasks.Task> UpdateOrderRows { get; internal set; }

        /// <summary>
        /// This method is used to replace all existing order rows with new order rows, assuming that the order has the action "CanUpdateOrderRow".
        /// The new order amount should not exceed the current order amount.
        /// This method will not work for partially delivered orders.
        /// </summary>
        public Func<ReplaceOrderRowsRequest, System.Threading.Tasks.Task> ReplaceOrderRows { get; internal set; }
    }
}
