using Svea.WebPay.SDK.PaymentAdminApi;
using System.Linq;

namespace Sample.AspNetCore.Helpers
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    public static  class ActionsValidationHelper
    {
        public static string ValidateOrderAction(Order order, string orderAction)
        {
            if (order == null)
            {
                return "Payment Order does not exist";
            }

            if (orderAction == null)
            {
                return null;
            }

            if (!order.AvailableActions.Contains(orderAction))
            {
                return "Operation not available";
            }

            return null;
        }

        public static string ValidateOrderRowAction(Order order, long orderRowId, string orderRowAction)
        {
            var orderError = ValidateOrderAction(order, null);

            if(orderError != null)
            {
                return orderError;
            }

            var orderRow = order.OrderRows.FirstOrDefault(row => row.OrderRowId == orderRowId);

            if (orderRow == null)
            {
                return "Order row does not exist";
            }

            if (orderRowAction == null)
            {
                return null;
            }

            if (!orderRow.AvailableActions.Contains(orderRowAction))
            {
                return "Operation not available";
            }

            return null;
        }

        public static string ValidateDeliveryAction(Order order, long deliveryId, string deliveryAction)
        {
            var orderError = ValidateOrderAction(order, null);

            if (orderError != null)
            {
                return orderError;
            }

            var delivery = order.Deliveries.FirstOrDefault(dlv => dlv.Id == deliveryId);

            if (delivery == null)
            {
                return "Order row does not exist";
            }

            if (deliveryAction == null)
            {
                return null;
            }

            if (!delivery.AvailableActions.Contains(deliveryAction))
            {
                return "Operation not available";
            }

            return null;
        }
    }
}
