using Svea.WebPay.SDK;
using Svea.WebPay.SDK.CheckoutApi;

namespace Sample.Testshop.Server.Modules.Checkout
{
    public class GetOrderHandler
    {
        public static async Task<Data> Handle(int orderId, SveaWebPayClient sveaClient)
        {
            var response = await sveaClient.Checkout.GetOrder(orderId);
            return response;

        }
    }
}
