using Atata;
using NUnit.Framework;
using Sample.AspNetCore.SystemTests.Services;
using Sample.AspNetCore.SystemTests.Test.Base;
using Sample.AspNetCore.SystemTests.Test.Helpers;
using Svea.WebPay.SDK.CheckoutApi;
using Svea.WebPay.SDK.PaymentAdminApi;
using System;
using System.Collections.Generic;
using System.Linq;
using Checkout = Sample.AspNetCore.SystemTests.Test.Helpers.Checkout;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Payment
{
    public class ShippingOrderTests : Base.PaymentTests
    {
        public ShippingOrderTests(string driverAlias)
            : base(driverAlias)
        {
        }

        [RetryWithException(2)]
        [Test(Description = "8226 - As a user I want to finalize a card purchase with the first shipping option")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void OrderWithShippingOptionWithCard(Product[] products)
        {
            var shipping = new Dictionary<string, string[]>
            {
                { "carrier", new string[] { ShippingOptions.Plab } }
            };

            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Card, shipping, false)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                        x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                        x.Orders.Count() > 0 &&
                        x.Orders.Last().Order.GetContent(ContentSource.TextContent).Contains("shipping.created"), 30, 5)

                    .Orders.Last().Order.OrderId.StoreValue(out var orderId)

                    // Validate order info
                    .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Open))
                    .Orders.Last().Order.PaymentType.Should.Equal(nameof(Svea.WebPay.SDK.PaymentAdminApi.PaymentType.Card))
                    .Orders.Last().Order.ShippingStatus.Should.Contain("shipping.created")
                    .Orders.Last().Order.ShippingDescription.Should.Contain(ShippingOptions.Plab)

                    // Validate deliveries info
                    .Orders.Last().Deliveries.Should.HaveCount(0)
                    .Orders.Last().Order.Table.Toggle.Click()
                    .Orders.Last().Order.Table.DeliverOrder.ClickAndGo();

                // Assert sdk/api response
                var response = await _sveaClientSweden.Checkout.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                var shippingOption = response.ShippingInformation.ShippingProvider.ShippingOption;

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(Svea.WebPay.SDK.CheckoutApi.PaymentType.SVEACARDPAY)));
                Assert.That(response.Status.ToString(), Is.EqualTo(nameof(CheckoutOrderStatus.Final)));

                Assert.That(response.ShippingAddress.FirstName, Is.EqualTo(TestDataService.SwedishFirstName));
                Assert.That(response.ShippingAddress.LastName, Is.EqualTo(TestDataService.SwedishLastName));
                Assert.That(response.ShippingAddress.StreetAddress, Is.EqualTo(TestDataService.ShippingStreetAddress));
                Assert.That(response.ShippingAddress.PostalCode, Is.EqualTo(TestDataService.ShippingZipCode.Replace(" ", "")));
                Assert.That(response.ShippingAddress.City.ToLower(), Is.EqualTo(TestDataService.ShippingCity.ToLower()));

                Assert.That(shippingOption.Carrier.ToLower(), Is.EqualTo(nameof(ShippingOptions.Plab).ToLower()));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCDELIVERYINSTRUCTIONS").Value, Is.EqualTo(TestDataService.ShippingInstructions));
                Assert.That(shippingOption.Addons.First(x => x.Id == "FCNOTIFYPHONE").Fields.First().Value.Take(3), Is.EqualTo(TestDataService.SwedishPhoneNumber.Take(3)));
                Assert.That(shippingOption.Addons.First(x => x.Id == "FCNOTIFYPHONE").Fields.First().Value.Skip(9), Is.EqualTo(TestDataService.SwedishPhoneNumber.Skip(9)));
                Assert.That(shippingOption.Addons.First(x => x.Id == "FCDELIVERYTIME12"), Is.Not.Null);
            });
        }

        [Ignore("Not needed")]
        [RetryWithException(2)]
        [Test(Description = "8227 - As a user I want to finalize a card purchase with the first shipping option, but change the place of delivery to the second option")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void OrderWithShippingOptionAndChangePickupOptionWithCard(Product[] products)
        {
            var shipping = new Dictionary<string, string[]>
            {
                { "carrier", new string[] { ShippingOptions.Plab, ShippingOptions.Bring } },
                { "pickup", new string[] { 1.ToString() } },
            };

            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Card, shipping, false)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                        x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                        x.Orders.Count() > 0 &&
                        x.Orders.Last().Order.GetContent(ContentSource.TextContent).Contains("shipping.created"), 30, 5)

                    .Orders.Last().Order.OrderId.StoreValue(out var orderId)

                    // Validate order info
                    .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Open))
                    .Orders.Last().Order.PaymentType.Should.Equal(nameof(Svea.WebPay.SDK.PaymentAdminApi.PaymentType.Card))
                    .Orders.Last().Order.ShippingStatus.Should.Contain("shipping.created")
                    .Orders.Last().Order.ShippingDescription.Should.Contain(ShippingOptions.Bring)

                    // Validate deliveries info
                    .Orders.Last().Deliveries.Should.HaveCount(0)
                    .Orders.Last().Order.Table.Toggle.Click()
                    .Orders.Last().Order.Table.DeliverOrder.ClickAndGo();

                // Assert sdk/api response
                var response = await _sveaClientSweden.Checkout.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                var shippingOption = response.ShippingInformation.ShippingProvider.ShippingOption;

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(Svea.WebPay.SDK.CheckoutApi.PaymentType.SVEACARDPAY)));
                Assert.That(response.Status.ToString(), Is.EqualTo(nameof(CheckoutOrderStatus.Final)));

                Assert.That(response.ShippingAddress.FirstName, Is.EqualTo(TestDataService.SwedishFirstName));
                Assert.That(response.ShippingAddress.LastName, Is.EqualTo(TestDataService.SwedishLastName));
                Assert.That(response.ShippingAddress.StreetAddress, Is.EqualTo(TestDataService.ShippingStreetAddress));
                Assert.That(response.ShippingAddress.PostalCode, Is.EqualTo(TestDataService.ShippingZipCode.Replace(" ", "")));
                Assert.That(response.ShippingAddress.City.ToLower(), Is.EqualTo(TestDataService.ShippingCity.ToLower()));

                Assert.That(shippingOption.Carrier.ToLower(), Is.EqualTo(nameof(ShippingOptions.Bring).ToLower()));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERDOORCODE").Value, Is.EqualTo(TestDataService.DoorCode));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERSMS").Value.Take(3), Is.EqualTo(TestDataService.SwedishPhoneNumber.Take(3)));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERSMS").Value.Skip(9), Is.EqualTo(TestDataService.SwedishPhoneNumber.Skip(9)));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCDELIVERYINSTRUCTIONS").Value, Is.EqualTo(TestDataService.ShippingInstructions));
            });
        }

        [Ignore("Not needed")]
        [RetryWithException(2)]
        [Test(Description = "8228 - As a user I want to finalize a card purchase with the first shipping option, but change the place of delivery to the last option")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void OrderWithShippingOptionAndChangeToLastPickupOptionWithCard(Product[] products)
        {
            var shipping = new Dictionary<string, string[]>
            {
                { "carrier", new string[] { ShippingOptions.Plab, ShippingOptions.Bring } },
                { "pickup", new string[] { (-1).ToString() } },
            };

            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Card, shipping, false)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                        x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                        x.Orders.Count() > 0 &&
                        x.Orders.Last().Order.GetContent(ContentSource.TextContent).Contains("shipping.created"), 30, 5)

                    .Orders.Last().Order.OrderId.StoreValue(out var orderId)

                    // Validate order info
                    .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Open))
                    .Orders.Last().Order.PaymentType.Should.Equal(nameof(Svea.WebPay.SDK.PaymentAdminApi.PaymentType.Card))
                    .Orders.Last().Order.ShippingStatus.Should.Contain("shipping.created")
                    .Orders.Last().Order.ShippingDescription.Should.Contain(ShippingOptions.Bring)

                    // Validate deliveries info
                    .Orders.Last().Deliveries.Should.HaveCount(0)
                    .Orders.Last().Order.Table.Toggle.Click()
                    .Orders.Last().Order.Table.DeliverOrder.ClickAndGo();

                // Assert sdk/api response
                var response = await _sveaClientSweden.Checkout.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                var shippingOption = response.ShippingInformation.ShippingProvider.ShippingOption;

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(Svea.WebPay.SDK.CheckoutApi.PaymentType.SVEACARDPAY)));
                Assert.That(response.Status.ToString(), Is.EqualTo(nameof(CheckoutOrderStatus.Final)));

                Assert.That(response.ShippingAddress.FirstName, Is.EqualTo(TestDataService.SwedishFirstName));
                Assert.That(response.ShippingAddress.LastName, Is.EqualTo(TestDataService.SwedishLastName));
                Assert.That(response.ShippingAddress.StreetAddress, Is.EqualTo(TestDataService.ShippingStreetAddress));
                Assert.That(response.ShippingAddress.PostalCode, Is.EqualTo(TestDataService.ShippingZipCode.Replace(" ", "")));
                Assert.That(response.ShippingAddress.City.ToLower(), Is.EqualTo(TestDataService.ShippingCity.ToLower()));

                Assert.That(shippingOption.Carrier.ToLower(), Is.EqualTo(nameof(ShippingOptions.Bring).ToLower()));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERDOORCODE").Value, Is.EqualTo(TestDataService.DoorCode));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERSMS").Value.Take(3), Is.EqualTo(TestDataService.SwedishPhoneNumber.Take(3)));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERSMS").Value.Skip(9), Is.EqualTo(TestDataService.SwedishPhoneNumber.Skip(9)));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCDELIVERYINSTRUCTIONS").Value, Is.EqualTo(TestDataService.ShippingInstructions));
                Assert.That(shippingOption.Addons.Count(), Is.EqualTo(0));
            });
        }

        [RetryWithException(2)]
        [Test(Description = "8229 - As a user I want to finalize an invoice purchase with the second shipping option")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void OrderWithShippingOptionWithInvoice(Product[] products)
        {
            var shipping = new Dictionary<string, string[]>
            {
                { "carrier", new string[] { ShippingOptions.Plab, ShippingOptions.Bring } }
            };

            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice, shipping, false)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                        x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                        x.Orders.Count() > 0 &&
                        x.Orders.Last().Order.GetContent(ContentSource.TextContent).Contains("shipping.created"), 30, 5)

                    .Orders.Last().Order.OrderId.StoreValue(out var orderId)

                    // Validate order info
                    .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Open))
                    .Orders.Last().Order.PaymentType.Should.Equal(nameof(Svea.WebPay.SDK.PaymentAdminApi.PaymentType.Invoice))
                    .Orders.Last().Order.ShippingStatus.Should.Contain("shipping.created")
                    .Orders.Last().Order.ShippingDescription.Should.Contain(ShippingOptions.Bring)

                    // Validate deliveries info
                    .Orders.Last().Deliveries.Should.HaveCount(0)
                    .Orders.Last().Order.Table.Toggle.Click()
                    .Orders.Last().Order.Table.DeliverOrder.ClickAndGo();

                // Assert sdk/api response
                var response = await _sveaClientSweden.Checkout.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                var shippingOption = response.ShippingInformation.ShippingProvider.ShippingOption;

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(Svea.WebPay.SDK.CheckoutApi.PaymentType.INVOICE)));
                Assert.That(response.Status.ToString(), Is.EqualTo(nameof(CheckoutOrderStatus.Final)));

                Assert.That(response.ShippingAddress.FirstName, Is.EqualTo(TestDataService.SwedishFirstName));
                Assert.That(response.ShippingAddress.LastName, Is.EqualTo(TestDataService.SwedishLastName));
                Assert.That(response.ShippingAddress.StreetAddress, Is.EqualTo(TestDataService.ShippingStreetAddress));
                Assert.That(response.ShippingAddress.PostalCode, Is.EqualTo(TestDataService.ShippingZipCode.Replace(" ", "")));
                Assert.That(response.ShippingAddress.City.ToLower(), Is.EqualTo(TestDataService.ShippingCity.ToLower()));

                Assert.That(shippingOption.Carrier.ToLower(), Is.EqualTo(nameof(ShippingOptions.Bring).ToLower()));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERDOORCODE").Value, Is.EqualTo(TestDataService.DoorCode));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERSMS").Value.Take(3), Is.EqualTo(TestDataService.SwedishPhoneNumber.Take(3)));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERSMS").Value.Skip(9), Is.EqualTo(TestDataService.SwedishPhoneNumber.Skip(9)));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCDELIVERYINSTRUCTIONS").Value, Is.EqualTo(TestDataService.ShippingInstructions));
                Assert.That(shippingOption.Addons.Count(), Is.EqualTo(0));
            });
        }

        [Ignore("Not needed")]
        [RetryWithException(2)]
        [Test(Description = "8230 - As a user I want to finalize an invoice purchase with the last shipping option")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void OrderWithLastShippingOptionWithInvoice(Product[] products)
        {
            var shipping = new Dictionary<string, string[]>
            {
                { "carrier", new string[] { ShippingOptions.Plab, ShippingOptions.Budbee } }
            };

            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice, shipping, false)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                        x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                        x.Orders.Count() > 0 &&
                        x.Orders.Last().Order.GetContent(ContentSource.TextContent).Contains("shipping.created"), 30, 5)

                    .Orders.Last().Order.OrderId.StoreValue(out var orderId)

                    // Validate order info
                    .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Open))
                    .Orders.Last().Order.PaymentType.Should.Equal(nameof(Svea.WebPay.SDK.PaymentAdminApi.PaymentType.Invoice))
                    .Orders.Last().Order.ShippingStatus.Should.Contain("shipping.created")
                    .Orders.Last().Order.ShippingDescription.Should.Contain(ShippingOptions.Budbee)

                    // Validate deliveries info
                    .Orders.Last().Deliveries.Should.HaveCount(0)
                    .Orders.Last().Order.Table.Toggle.Click()
                    .Orders.Last().Order.Table.DeliverOrder.ClickAndGo();

                // Assert sdk/api response
                var response = await _sveaClientSweden.Checkout.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                var shippingOption = response.ShippingInformation.ShippingProvider.ShippingOption;

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(Svea.WebPay.SDK.CheckoutApi.PaymentType.INVOICE)));
                Assert.That(response.Status.ToString(), Is.EqualTo(nameof(CheckoutOrderStatus.Final)));

                Assert.That(response.ShippingAddress.FirstName, Is.EqualTo(TestDataService.SwedishFirstName));
                Assert.That(response.ShippingAddress.LastName, Is.EqualTo(TestDataService.SwedishLastName));
                Assert.That(response.ShippingAddress.StreetAddress, Is.EqualTo(TestDataService.ShippingStreetAddress));
                Assert.That(response.ShippingAddress.PostalCode, Is.EqualTo(TestDataService.ShippingZipCode.Replace(" ", "")));
                Assert.That(response.ShippingAddress.City.ToLower(), Is.EqualTo(TestDataService.ShippingCity.ToLower()));

                Assert.That(shippingOption.Carrier.ToLower().Contains(nameof(ShippingOptions.Budbee).ToLower()), Is.True);
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERPHONE").Value.Take(3), Is.EqualTo(TestDataService.SwedishPhoneNumber.Take(3)));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERPHONE").Value.Skip(9), Is.EqualTo(TestDataService.SwedishPhoneNumber.Skip(9)));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERSMS").Value.Take(3), Is.EqualTo(TestDataService.SwedishPhoneNumber.Take(3)));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERSMS").Value.Skip(9), Is.EqualTo(TestDataService.SwedishPhoneNumber.Skip(9)));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCDELIVERYINSTRUCTIONS").Value, Is.EqualTo(TestDataService.ShippingInstructions));
                Assert.That(shippingOption.Addons.Count(), Is.EqualTo(0));
            });
        }

        [Ignore("Not needed")]
        [RetryWithException(2)]
        [Test(Description = "8231 - As a user I want to finalize an invoice purchase with the last shipping option, and change place of delivery to the last option")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void OrderWithLastShippingOptionAndLastPickupOptionWithInvoice(Product[] products)
        {
            var shipping = new Dictionary<string, string[]>
            {
                { "carrier", new string[] { ShippingOptions.Plab, ShippingOptions.Dhl } },
                { "pickup", new string[] { (-1).ToString() } },
            };

            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice, shipping, false)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                        x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                        x.Orders.Count() > 0 &&
                        x.Orders.First().Order.GetContent(ContentSource.TextContent).Contains("shipping.created"), 30, 5)

                    .Orders.First().Order.OrderId.StoreValue(out var orderId)

                    // Validate order info
                    .Orders.First().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Open))
                    .Orders.First().Order.PaymentType.Should.Equal(nameof(Svea.WebPay.SDK.PaymentAdminApi.PaymentType.Invoice))
                    .Orders.First().Order.ShippingStatus.Should.Contain("shipping.created")
                    .Orders.First().Order.ShippingDescription.Should.Contain(ShippingOptions.Dhl)

                    // Validate deliveries info
                    .Orders.Last().Deliveries.Should.HaveCount(0)
                    .Orders.Last().Order.Table.Toggle.Click()
                    .Orders.Last().Order.Table.DeliverOrder.ClickAndGo();

                // Assert sdk/api response
                var response = await _sveaClientSweden.Checkout.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                var shippingOption = response.ShippingInformation.ShippingProvider.ShippingOption;

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(Svea.WebPay.SDK.CheckoutApi.PaymentType.INVOICE)));
                Assert.That(response.Status.ToString(), Is.EqualTo(nameof(CheckoutOrderStatus.Final)));

                Assert.That(response.ShippingAddress.FirstName, Is.EqualTo(TestDataService.SwedishFirstName));
                Assert.That(response.ShippingAddress.LastName, Is.EqualTo(TestDataService.SwedishLastName));
                Assert.That(response.ShippingAddress.StreetAddress, Is.EqualTo(TestDataService.ShippingStreetAddress));
                Assert.That(response.ShippingAddress.PostalCode, Is.EqualTo(TestDataService.ShippingZipCode.Replace(" ", "")));
                Assert.That(response.ShippingAddress.City.ToLower(), Is.EqualTo(TestDataService.ShippingCity.ToLower()));

                Assert.That(shippingOption.Carrier.ToLower().Contains(nameof(ShippingOptions.Dhl).ToLower()), Is.True);
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERPHONE").Value.Take(3), Is.EqualTo(TestDataService.SwedishPhoneNumber.Take(3)));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERPHONE").Value.Skip(9), Is.EqualTo(TestDataService.SwedishPhoneNumber.Skip(9)));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERSMS").Value.Take(3), Is.EqualTo(TestDataService.SwedishPhoneNumber.Take(3)));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERSMS").Value.Skip(9), Is.EqualTo(TestDataService.SwedishPhoneNumber.Skip(9)));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCRECEIVERDOORCODE").Value, Is.EqualTo(TestDataService.DoorCode));
                Assert.That(shippingOption.Fields.First(x => x.Id == "FCDELIVERYINSTRUCTIONS").Value, Is.EqualTo(TestDataService.ShippingInstructions));
                Assert.That(shippingOption.Addons.Count(), Is.EqualTo(0));
            });
        }

    }
}
