using Sample.Testshop.Server.Modules.Checkout.Models;
using Svea.WebPay.SDK;
using Svea.WebPay.SDK.CheckoutApi;
using Svea.WebPay.SDK.CheckoutApi.Recurring;
using System.Globalization;
using Cart = Sample.Testshop.Server.Modules.Checkout.Models.Cart;

namespace Sample.Testshop.Server.Modules.Checkout
{
    public static class CreateRecurringOrderHandler
    {
        public static async Task<OrderData> Handle(CreateRecurringOrderRequest createRecurringOrderModel, string token, SveaWebPayClient sveaClient)
        {
            if (string.IsNullOrWhiteSpace(createRecurringOrderModel?.ClientOrderNumber))
            {
                createRecurringOrderModel.ClientOrderNumber = Guid.NewGuid().ToString().Replace("-", "");
                createRecurringOrderModel.Currency = "SEK";
            }


            var response = await sveaClient.Checkout.Recurring.CreateRecurringOrderAsync(new CreateRecurringOrderModel(createRecurringOrderModel.ClientOrderNumber, createRecurringOrderModel.Currency,
                createRecurringOrderModel.MerchantSettings.ToSDKModel(), createRecurringOrderModel.Cart.ToSDKModel()), token);

            return response;

        }
        public class CreateRecurringOrderRequest
        {
            public string ClientOrderNumber { get; set; }
            public string Currency { get; set; }
            public Cart Cart { get; set; }
            public RecurringMerchantSettingsRequest MerchantSettings { get; set; }
        }
        public class RecurringMerchantSettingsRequest
        {
            public string CheckoutValidationCallBackUri { get; set; }
            public string PushUri { get; set; }
            public RecurringMerchantSettings ToSDKModel()
            {
                return new RecurringMerchantSettings()
                {
                    PushUri = new Uri(PushUri),
                    CheckoutValidationCallBackUri = !string.IsNullOrEmpty(CheckoutValidationCallBackUri) ? new Uri(CheckoutValidationCallBackUri) : null,
                };
            }
        }
    }
}
