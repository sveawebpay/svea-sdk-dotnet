using Atata;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using Sample.AspNetCore.SystemTests.PageObjectModels;
using Sample.AspNetCore.SystemTests.PageObjectModels.Orders;
using Sample.AspNetCore.SystemTests.PageObjectModels.ThankYou;
using Sample.AspNetCore.SystemTests.Services;
using Sample.AspNetCore.SystemTests.Test.Base;
using Sample.AspNetCore.SystemTests.Test.Helpers;
using Svea.WebPay.SDK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Base
{
    public abstract class PaymentTests : TestBase
    {
        private string _amount;

        public PaymentTests(string driverAlias)
            : base(driverAlias)
        {
        }

        protected SveaWebPayClient _sveaClient;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var config = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .AddUserSecrets("6343ea20-120b-4075-a141-c8154cad1d14")
             .AddEnvironmentVariables()
             .Build();

            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };

            var checkoutApihttpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(config.GetSection("SveaApiUrls").GetSection("CheckoutApiUri").Value)
            };
            var paymentAdminApiHttpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(config.GetSection("SveaApiUrls").GetSection("PaymentAdminApiUri").Value)
            };

            _sveaClient = new SveaWebPayClient(
                checkoutApihttpClient,
                paymentAdminApiHttpClient,
                new Credentials(
                    config.GetSection("Credentials").GetSection("MerchantId").Value,
                    config.GetSection("Credentials").GetSection("Secret").Value
                )
            );
        }

        protected ProductsPage SelectProducts(Product[] products)
        {
            return Go.To<ProductsPage>()
                .Do((x) =>
                {
                    if (x.Header.ClearOrders.Exists(new SearchOptions { Timeout = TimeSpan.FromSeconds(2), IsSafely = true }))
                    {
                        x
                        .Header.ClearOrders.ClickAndGo()
                        .Header.Products.ClickAndGo();
                    }

                    foreach (var product in products)
                    {
                        product.Name = x.Products.Rows[y => !products.Any(p => p.Name == y.Name.Value)].Name.Value;

                        x
                        .Products.Rows[y => y.Name.Value == product.Name].AddToCart.ClickAndGo<ProductsPage>()
                        .Products.Rows[y => y.Name.Value == product.Name].Price.StoreNumericalValue(out var price, characterToRemove: " ")
                        .Products.Rows[y => y.Name.Value == product.Name].Price.StoreCurrency(out var currency, characterToRemove: " ");

                        if (product.Quantity != 1)
                        {
                            x
                            .CartProducts.Rows[y => y.Name.Value == product.Name].Quantity.Set(product.Quantity)
                            .CartProducts.Rows[y => y.Name.Value == product.Name].Update.Click();
                        }

                        product.UnitPrice = price;
                        product.Currency = currency;
                    }

                    _amount = $"{ products.Sum(p => p.UnitPrice * p.Quantity) } {products.First().Currency}";
                });
        }

        protected SveaPaymentFramePage GoToSveaPaymentFrame(Product[] products)
        {
            return SelectProducts(products)
                .AnonymousCheckout.ClickAndGo()
                .SveaFrame.SwitchTo<SveaPaymentFramePage>();
        }

        protected ThankYouPage GoToThankYouPage(Product[] products, Checkout.Option checkout = Checkout.Option.Identification, Entity.Option entity = Entity.Option.Private, PaymentMethods.Option paymentMethod = PaymentMethods.Option.Card)
        {
            var page = GoToSveaPaymentFrame(products);

            try
            {
                page.IdentifyEntity(checkout, entity);
            }
            catch(StaleElementReferenceException)
            {
                page.RefreshPage()
                    .SwitchToFrame<SveaPaymentFramePage>(By.Id("svea-checkout-iframe"))
                    .IdentifyEntity(checkout, entity);
            }
                
            page.Pay(checkout, entity, paymentMethod, _amount);

            try
            {
                return page
                    .PageUrl.WaitTo.Within(TimeSpan.FromSeconds(60)).Contain("thankyou")
                    .SwitchToRoot<ThankYouPage>()
                    .ThankYou.IsVisible.WaitTo.BeTrue();
            }
            catch (StaleElementReferenceException)
            {
                return Go.To<ThankYouPage>();
            }
        }

        protected OrdersPage GoToOrdersPage(Product[] products, Checkout.Option checkout = Checkout.Option.Identification, Entity.Option entity = Entity.Option.Private, PaymentMethods.Option paymentMethod = PaymentMethods.Option.Card)
        {
            return GoToThankYouPage(products, checkout, entity, paymentMethod)
                .RefreshPageUntil(x => x.Header.Orders.IsVisible.Value == true, timeout: 25, retryInterval: 3)
                .Header.Orders.ClickAndGo();
        }

        protected static IEnumerable TestData(bool singleProduct = true)
        {
            var data = new List<object>();

            if (singleProduct)
                data.Add(new[]
                {
                    new Product { Quantity = 1 }
                });
            else
                data.Add(new[]
                {
                    new Product { Quantity = 3 },
                    new Product { Quantity = 2 }
                });

            yield return data.ToArray();
        }
    }
}
