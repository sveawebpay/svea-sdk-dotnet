using Atata;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
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
                .SveaFrame.SwitchTo<SveaPaymentFramePage>()
                .Entity.IsVisible.WaitTo.BeTrue();
        }

        protected SveaPaymentFramePage ProceedWithPrivateIdentification(Product[] products)
        {
            return GoToSveaPaymentFrame(products)
                .Entity.IsPrivate.Click()
                .B2CIdentification.Email.Set(TestDataService.Email)
                .B2CIdentification.ZipCode.Set(TestDataService.ZipCode)
                .Submit.Click()
                .Do(x =>
                {
                    if (x.B2CIdentification.PersonalNumber.Exists(new SearchOptions { IsSafely = true }))
                    {
                        x
                        .B2CIdentification.PersonalNumber.Set(TestDataService.PersonalNumber)
                        .B2CIdentification.Phone.Set(TestDataService.PhoneNumber)
                        .Submit.Click();
                    }
                });

        }

        protected SveaPaymentFramePage ProceedWithCompanyIdentification(Product[] products)
        {
            return GoToSveaPaymentFrame(products)
                .Entity.IsCompany.Click()
                .B2BIdentification.Email.Set(TestDataService.Email)
                .B2BIdentification.OrganizationNumber.Set(TestDataService.OrganizationNumber)
                .Submit.Click()
                .Do(x =>
                {
                    if (x.B2BIdentification.Phone.Exists(new SearchOptions { IsSafely = true }))
                    {
                        x
                        .B2BIdentification.Phone.Set(TestDataService.PhoneNumber)
                        .Submit.Click();
                    }
                });
        }

        protected SveaPaymentFramePage ProceedWithPrivateAnonymous(Product[] products)
        {
            return GoToSveaPaymentFrame(products)
                .Entity.IsPrivate.Click()
                .Entity.ToggleIdentification.Click()
                .B2CAnonymous.IsVisible.WaitTo.BeTrue()
                .B2CAnonymous.FirstName.Set(TestDataService.FirstName)
                .B2CAnonymous.LastName.Set(TestDataService.LastName)
                .B2CAnonymous.Street.Set(TestDataService.Street)
                .B2CAnonymous.CareOfToggle.Click()
                .B2CAnonymous.CareOf.Set(TestDataService.CareOf)
                .B2CAnonymous.ZipCode.Set(TestDataService.ZipCode)
                .B2CAnonymous.City.Set(TestDataService.City)
                .B2CAnonymous.Email.Set(TestDataService.Email)
                .B2CAnonymous.PhoneNumber.Set(TestDataService.PhoneNumber)
                .Submit.Click();
        }

        protected SveaPaymentFramePage ProceedWithCompanyAnonymous(Product[] products)
        {
            return GoToSveaPaymentFrame(products)
                .Entity.IsCompany.Click()
                .Entity.ToggleIdentification.Click()
                .B2BAnonymous.IsVisible.WaitTo.BeTrue()
                .B2BAnonymous.OrganizationName.Set(TestDataService.CompanyName)
                .B2BAnonymous.Street.Set(TestDataService.Street)
                .B2BAnonymous.ZipCode.Set(TestDataService.ZipCode)
                .B2BAnonymous.City.Set(TestDataService.City)
                .B2BAnonymous.Email.Set(TestDataService.Email)
                .B2BAnonymous.PhoneNumber.Set(TestDataService.PhoneNumber)
                .Submit.Click();
        }

        protected SveaPaymentFramePage GoToSveaPaymentMethod(Product[] products, Checkout.Option checkout = Checkout.Option.Identification, Entity.Option entity = Entity.Option.Private)
        {
            switch (entity)
            {
                default:
                case Entity.Option.Private:

                    switch (checkout)
                    {
                        default:
                        case Checkout.Option.Identification:
                            return ProceedWithPrivateIdentification(products);

                        case Checkout.Option.Anonymous:
                            return ProceedWithPrivateAnonymous(products);
                    }

                case Entity.Option.Company:

                    switch (checkout)
                    {
                        default:
                        case Checkout.Option.Identification:
                            return ProceedWithCompanyIdentification(products);

                        case Checkout.Option.Anonymous:
                            return ProceedWithCompanyAnonymous(products);
                    }
            }
        }

        protected ThankYouPage GoToThankYouPage(Product[] products, Checkout.Option checkout = Checkout.Option.Identification, Entity.Option entity = Entity.Option.Private, PaymentMethods.Option paymentMethod = PaymentMethods.Option.Card)
        {
            var page = GoToSveaPaymentMethod(products, checkout, entity)
                .PaymentMethods.IsVisible.WaitTo.BeTrue()
                .PaymentMethods.TotalAmount.IsVisible.WaitTo.BeTrue()
                .PaymentMethods.TotalAmount.Should.ContainAmount(_amount);

            if (entity == Entity.Option.Company && checkout == Checkout.Option.Identification)
            {
                page.PaymentMethods.Reference.Set(TestDataService.Reference);
            }

            switch (paymentMethod)
            {
                default:
                case PaymentMethods.Option.Card:
                    return page.PayWithCard();

                case PaymentMethods.Option.DirektBank:
                    return page.PayWithDirektBank();

                case PaymentMethods.Option.Invoice:
                    return page.PayWithInvoice();

                case PaymentMethods.Option.Trustly:
                    return page.PayWithTrustly(checkout);

                case PaymentMethods.Option.PaymentPlan:
                    return page.PayWithPaymentPlan();

                case PaymentMethods.Option.AccountCredit:
                    return page.PayWithAccount();

                case PaymentMethods.Option.BlackFriday:
                    return page.PayWithBlackFriday();

                case PaymentMethods.Option.Swish:
                    return page.PayWithSwish();
            }
        }

        protected OrdersPage GoToOrdersPage(Product[] products, Checkout.Option checkout = Checkout.Option.Identification, Entity.Option entity = Entity.Option.Private, PaymentMethods.Option paymentMethod = PaymentMethods.Option.Card)
        {
            return GoToThankYouPage(products, checkout, entity, paymentMethod)
                .PageUrl.WaitTo.Within(TimeSpan.FromSeconds(60)).Contain("thankyou")
                .SwitchToRoot<ThankYouPage>()
                .ThankYou.IsVisible.WaitTo.BeTrue()
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
