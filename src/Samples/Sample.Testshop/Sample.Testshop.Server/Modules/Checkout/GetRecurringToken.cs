using Svea.WebPay.SDK;
using Svea.WebPay.SDK.CheckoutApi.Recurring;

namespace Sample.Testshop.Server.Modules.Checkout
{
    public static class GetRecurringToken
    {
        public static async Task<RecurringToken> Handle(string token, SveaWebPayClient sveaWebPayClient)
        {
            var response = await sveaWebPayClient.Checkout.Recurring.GetRecurringTokenAsync(token);
            return response;

        }
    }
}
