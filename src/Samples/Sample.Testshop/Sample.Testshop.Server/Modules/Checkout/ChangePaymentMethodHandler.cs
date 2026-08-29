using Svea.WebPay.SDK;
using Svea.WebPay.SDK.CheckoutApi.Recurring;

namespace Sample.Testshop.Server.Modules.Checkout
{
    public static class ChangePaymentMethodHandler
    {
        public static async Task<ChangePaymentMethodResponse> Handle(string token, ChangepaymentMethodModel request, SveaWebPayClient sveaWebPayClient)
        {
            var response = await sveaWebPayClient.Checkout.Recurring.ChangePaymentMethodAsync(request, token);
            return response;
        }
    }
}
