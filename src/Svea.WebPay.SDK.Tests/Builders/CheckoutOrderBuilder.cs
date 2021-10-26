namespace Svea.WebPay.SDK.Tests.Builders
{
    using Svea.WebPay.SDK.CheckoutApi;

    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class CheckoutOrderBuilder
    {
        private RegionInfo countryCode;
        private CurrencyCode currency;
        private Language locale;
        private string clientOrderNumber;
        private MerchantSettings merchantSettings;
        private Cart cart;

        public CreateOrderModel Build()
        {
            return new CreateOrderModel(this.countryCode, this.currency, this.locale, this.clientOrderNumber, this.merchantSettings, this.cart, false);
        }

        public CheckoutOrderBuilder UseTestValues()
        {
            var pushUri = new Uri("https://svea.com/push.aspx?sid=123&svea_order=123");
            var termsUri = new Uri("http://localhost:51898/terms");
            var checkoutUri = new Uri("http://localhost:8080/php-checkout/examples/create-order.php");
            var confirmationUri = new Uri("http://localhost/php-checkout/examples/get-order.php");
            
            var orderRows = new List<OrderRow>
            {
                new OrderRow(
                    "ABC80",
                    "Computer",
                    new MinorUnit(10),
                    new MinorUnit(5000),
                    new MinorUnit(10),
                    new MinorUnit(25),
                    null,
                    null,
                    1)
            };

            this.cart = new Cart(orderRows);
            this.merchantSettings = new MerchantSettings(pushUri, termsUri, checkoutUri, confirmationUri);
            this.countryCode = new RegionInfo("SE");
            this.currency = new CurrencyCode("SEK");
            this.locale = new Language("sv-SE");
            this.clientOrderNumber = DateTime.Now.Ticks.ToString();

            return this;
        }
    }
}
