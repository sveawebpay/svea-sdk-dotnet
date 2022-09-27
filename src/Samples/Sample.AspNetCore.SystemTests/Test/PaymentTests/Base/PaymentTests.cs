using Atata;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using Sample.AspNetCore.SystemTests.PageObjectModels;
using Sample.AspNetCore.SystemTests.PageObjectModels.Orders;
using Sample.AspNetCore.SystemTests.PageObjectModels.Payment;
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
using System.Text.RegularExpressions;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Base
{
    public abstract class PaymentTests : TestBase
    {
        private string _amountStr;
        protected double _amount;
        protected string _orderId;

        public PaymentTests(string driverAlias)
            : base(driverAlias)
        {
        }

        protected SveaWebPayClient _sveaClientSweden;
        protected SveaWebPayClient _sveaClientNorway;

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

            _sveaClientSweden = new SveaWebPayClient(
                checkoutApihttpClient,
                paymentAdminApiHttpClient,
                new Credentials(
                    config.GetSection("Credentials").GetChildren().First(x => x.GetSection("MarketId").Value == "SE").GetSection("MerchantId").Value,
                    config.GetSection("Credentials").GetChildren().First(x => x.GetSection("MarketId").Value == "SE").GetSection("Secret").Value
                )
            );

            _sveaClientNorway = new SveaWebPayClient(
                checkoutApihttpClient,
                paymentAdminApiHttpClient,
                new Credentials(
                    config.GetSection("Credentials").GetChildren().First(x => x.GetSection("MarketId").Value == "NO").GetSection("MerchantId").Value,
                    config.GetSection("Credentials").GetChildren().First(x => x.GetSection("MarketId").Value == "NO").GetSection("Secret").Value
                )
            );
        }

        protected ProductsPage SelectProducts(Product[] products, PaymentMethods.Option paymentMethod = PaymentMethods.Option.Card)
        {
            _amount = 0;

            return Go.To<ProductsPage>()
                .Do((x) =>
                {
                    if (x.Header.ClearOrders.Exists(new SearchOptions { Timeout = TimeSpan.FromSeconds(2), IsSafely = true }))
                    {
                        x
                        .Header.ClearOrders.ClickAndGo()
                        .Header.Products.ClickAndGo();
                    }

                    switch(paymentMethod)
                    {
                        case PaymentMethods.Option.Vipps:
                            x.Market.Click();
                            x.Markets[m => m.Content.Value == "NO"].Click();
                            x.Country.Click();
                            x.Countries[m => m.Content.Value == "NO"].Click();
                            break;
                        case PaymentMethods.Option.MobilePay:
                            x.Market.Click();
                            x.Markets[m => m.Content.Value == "FI"].Click();
                            break;
                    }
                    
                    foreach(var product in products)
                    {
                        product.Name = null;
                    }

                    foreach (var product in products)
                    {
                        if (product.HasAmountDiscount)
                        {
                            product.Name = x.Products.Rows[y => !products.Any(p => p.Name == y.Name.Value) && product.HasAmountDiscount == !string.IsNullOrWhiteSpace(y.AmountDiscount.Value)].Name.Value;
                        }
                        else if(product.HasPercentDiscount)
                        {
                            product.Name = x.Products.Rows[y => !products.Any(p => p.Name == y.Name.Value) && product.HasPercentDiscount == !string.IsNullOrWhiteSpace(y.PercentDiscount.Value)].Name.Value;
                        }
                        else
                        {
                            product.Name = x.Products.Rows[y => !products.Any(p => p.Name == y.Name.Value) && product.HasAmountDiscount == !string.IsNullOrWhiteSpace(y.AmountDiscount.Value) && product.HasPercentDiscount == !string.IsNullOrWhiteSpace(y.PercentDiscount.Value)].Name.Value;
                        }
                        
                        x
                        .Products.Rows[y => y.Name.Value == product.Name].AddToCart.ClickAndGo<ProductsPage>()
                        .Products.Rows[y => y.Name.Value == product.Name].Price.StoreNumericalValue(out var price, characterToRemove: " ")
                        .Products.Rows[y => y.Name.Value == product.Name].Price.StoreCurrency(out var currency, characterToRemove: " ")
                        .Products.Rows[y => y.Name.Value == product.Name].AmountDiscount.StoreNumericalValue(out var amountDiscount, characterToRemove: " ")
                        .Products.Rows[y => y.Name.Value == product.Name].PercentDiscount.StoreNumericalValue(out var percentDiscount, characterToRemove: "%");

                        if (product.Quantity != 1)
                        {
                            x
                            .CartProducts.Rows[y => y.Name.Value == product.Name].Quantity.Set(product.Quantity)
                            .CartProducts.Rows[y => y.Name.Value == product.Name].Update.Click();
                        }

                        product.UnitPrice = price;
                        product.Currency = currency;

                        double discount = 0;
                        
                        if(!string.IsNullOrEmpty(amountDiscount.ToString()) && amountDiscount != 0)
                        {
                            discount = double.Parse(amountDiscount.ToString());
                        }
                        else if (!string.IsNullOrEmpty(percentDiscount.ToString()) && percentDiscount != 0)
                        {
                            discount = double.Parse(percentDiscount.ToString()) * (product.UnitPrice * product.Quantity) / 100;
                        }

                        _amount += (product.UnitPrice * product.Quantity) - discount;
                    }

                    _amountStr = $"{_amount} {products.First().Currency}";
                });
        }

        protected SveaPaymentFramePage GoToSveaPaymentFrame(Product[] products, bool requireBankId = false, bool isInternational = false, PaymentMethods.Option paymentMethod = PaymentMethods.Option.Card)
        {
            Frame<SveaPaymentFramePage, PaymentPage> frame;

            if (requireBankId)
            {
                frame = SelectProducts(products, paymentMethod)
                    .CheckoutAndRequireBankId.ClickAndGo()
                    .SveaFrame;
            }
            else if (isInternational)
            {
                frame = SelectProducts(products, paymentMethod)
                    .InternationalCheckout.ClickAndGo()
                    .SveaFrame;
            }
            else 
            {
                frame = SelectProducts(products, paymentMethod)
                    .AnonymousCheckout.ClickAndGo()
                    .SveaFrame;
            }

            var match = Regex.Match(frame.Attributes.Src.Value, "orderId=(\\d+)");

            _orderId = match?.Groups?.Count > 1 ? match.Groups[1].Value : null;

            return frame.SwitchTo<SveaPaymentFramePage>(); ;
        }

        protected SveaPaymentFramePage GoToBankId(Product[] products, Checkout.Option checkout = Checkout.Option.Identification, Entity.Option entity = Entity.Option.Private, PaymentMethods.Option paymentMethod = PaymentMethods.Option.Card)
        {
            var page = GoToSveaPaymentFrame(products, requireBankId: true, isInternational: false, paymentMethod);

            try
            {
                page.IdentifyEntity(checkout, entity, paymentMethod);
            }
            catch (StaleElementReferenceException)
            {
                page.RefreshPage()
                    .SwitchToFrame<SveaPaymentFramePage>(By.Id("svea-checkout-iframe"))
                    .IdentifyEntity(checkout, entity, paymentMethod);
            }

            switch(paymentMethod)
            {
                case PaymentMethods.Option.Invoice:
                    page
                        .PaymentMethods.Invoice.IsVisible.WaitTo.BeTrue()
                        .PaymentMethods.Invoice.Click()
                        .Submit.Click()
                        .BankId.Should.BeVisible();
                    break;

                case PaymentMethods.Option.PaymentPlan:
                    page
                        .PaymentMethods.PaymentPlan.IsVisible.WaitTo.BeTrue()
                        .PaymentMethods.PaymentPlan.Click()
                        .WaitSeconds(1)
                        .PaymentMethods.PaymentPlan.Options[1].Click()
                        .Submit.Click()
                        .BankId.Should.BeVisible();
                    break;


                case PaymentMethods.Option.AccountCredit:
                    page
                        .PaymentMethods.Account.IsVisible.WaitTo.BeTrue()
                        .PaymentMethods.Account.Click()
                        .Submit.Click()
                        .BankId.Should.BeVisible();
                    break;
            }

            return page;
        }

        protected ThankYouPage GoToThankYouPage(Product[] products, Checkout.Option checkout = Checkout.Option.Identification, Entity.Option entity = Entity.Option.Private, PaymentMethods.Option paymentMethod = PaymentMethods.Option.Card, bool requireBankId = false)
        {
            var page = GoToSveaPaymentFrame(products, requireBankId: requireBankId, isInternational: false, paymentMethod);

            try
            {
                page.IdentifyEntity(checkout, entity, paymentMethod);
            }
            catch(StaleElementReferenceException)
            {
                page.RefreshPage()
                    .SwitchToFrame<SveaPaymentFramePage>(By.Id("svea-checkout-iframe"))
                    .IdentifyEntity(checkout, entity, paymentMethod);
            }
            
            page.Pay(checkout, entity, paymentMethod, _amountStr);

            try
            {
                return page
                    .PageUrl.Should.Within(TimeSpan.FromSeconds(60)).Contain("thankyou")
                    .SwitchToRoot<ThankYouPage>()
                    .ThankYou.IsVisible.WaitTo.BeTrue();
            }
            catch (StaleElementReferenceException)
            {
                return Go.To<ThankYouPage>();
            }
        }

        protected OrdersPage GoToOrdersPage(Product[] products, Checkout.Option checkout = Checkout.Option.Identification, Entity.Option entity = Entity.Option.Private, PaymentMethods.Option paymentMethod = PaymentMethods.Option.Card, bool requireBankId = false)
        {
            return GoToThankYouPage(products, checkout, entity, paymentMethod, requireBankId)
                .Do(x => 
                {
                    switch (paymentMethod)
                    {
                        case PaymentMethods.Option.Vipps:
                            x.WaitSeconds(1).Market.Click()
                            .Markets[m => m.Content.Value == "NO"].Click();
                            break;
                        case PaymentMethods.Option.MobilePay:
                            x.WaitSeconds(1).Market.Click()
                            .Markets[m => m.Content.Value == "FI"].Click();
                            break;
                        default:
                            x.WaitSeconds(1).Market.Click()
                            .Markets[m => m.Content.Value == "SE"].Click();
                            break;

                    }
                })
                .RefreshPageUntil(x => x.Header.Orders.IsVisible.Value == true, timeout: 25, retryInterval: 3)
                .Header.Orders.ClickAndGo();
        }

        protected static IEnumerable TestData(bool singleProduct = true, bool hasAmountDiscount = false, bool hasPercentDiscount = false, bool manySameArticle = false)
        {
            var data = new List<object>();

            if (singleProduct)
            {
                data.Add(new[]
                   {
                    new Product { Quantity = 1, HasAmountDiscount = hasAmountDiscount, HasPercentDiscount = hasPercentDiscount }
                });
            }
            else if(manySameArticle)
            {
                data.Add(new[]
                   {
                    new Product { Quantity = 8, HasAmountDiscount = hasAmountDiscount, HasPercentDiscount = hasPercentDiscount }
                });
            }    
            else
                data.Add(new[]
                {
                    new Product { Quantity = 3, HasAmountDiscount = hasAmountDiscount },
                    new Product { Quantity = 2, HasPercentDiscount = hasPercentDiscount }
                });

            yield return data.ToArray();
        }
    }
}
