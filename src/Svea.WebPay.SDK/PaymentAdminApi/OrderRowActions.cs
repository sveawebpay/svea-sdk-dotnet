using System;

namespace Svea.WebPay.SDK.PaymentAdminApi
{
    using Svea.WebPay.SDK.PaymentAdminApi.Request;
    using Svea.WebPay.SDK.PaymentAdminApi.Response;

    public class OrderRowActions
    {
        public OrderRowActions(long orderId, OrderRowResponseObject orderRowResponse, SveaHttpClient client)
        {
            if (orderRowResponse.Actions == null)
            {
                return;
            }

            foreach (var action in orderRowResponse.Actions)
            {
                switch (action)
                {
                    case OrderRowActionType.CanCancelRow:
                        CancelOrderRow = async payload => await client.HttpPatch<object>(
                            new Uri($"/api/v1/orders/{orderId}/rows/{orderRowResponse.OrderRowId}",
                                UriKind.Relative), payload, payload.ConfigureAwait).ConfigureAwait(payload.ConfigureAwait);
                        break;
                    case OrderRowActionType.CanUpdateRow:
                        UpdateOrderRow = async payload => await client.HttpPatch<object>(
                            new Uri($"/api/v1/orders/{orderId}/rows/{orderRowResponse.OrderRowId}",
                                UriKind.Relative), payload, payload.ConfigureAwait).ConfigureAwait(payload.ConfigureAwait);
                        break;
                }
            }
        }


        /// <summary>
        /// Changes the status of an order row to “Cancelled”, assuming the order has the action
        /// “CanCancelOrderRow” and the OrderRow has the action "CanCancelRow". 
        /// </summary>
        public Func<CancelRequest, System.Threading.Tasks.Task> CancelOrderRow { get; internal set; }

        /// <summary>
        /// This method is used to update an order row, assuming the order has action
        /// "CanUpdateOrderRow" and the order row has the action “CanUpdateRow”.
        /// The method will update all fields set in the payload, if a field is not set the row will keep the
        /// current value.
        /// If the new order amount will exceed the current order amount, a credit check will be taken.
        /// </summary>
        public Func<UpdateOrderRowRequest, System.Threading.Tasks.Task> UpdateOrderRow { get; internal set; }
    }
}
