using Svea.WebPay.SDK;
using Svea.WebPay.SDK.CheckoutApi;
using Svea.WebPay.SDK.CheckoutApi.Recurring;

namespace Sample.Testshop.Server.Modules.Checkout
{
    public static class GetRecurringOrder
    {
        public static async Task<RecurringOrder> Handle(string token, long orderId, SveaWebPayClient sveaWebPayClient)
        {
            var response = await sveaWebPayClient.Checkout.Recurring.GetRecurringOrderAsync(token, orderId);
            return response;
        }
    }
}
