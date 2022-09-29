using Atata;
using NUnit.Framework;
using Sample.AspNetCore.SystemTests.Services;
using Sample.AspNetCore.SystemTests.Test.Base;
using Sample.AspNetCore.SystemTests.Test.Helpers;
using Svea.WebPay.SDK.PaymentAdminApi;
using System;
using System.Linq;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Payment
{
    public class CardOrderTests : Base.PaymentTests
    {
        public CardOrderTests(string driverAlias)
            : base(driverAlias)
        {
        }

        [RetryWithException(2)]
        [Test(Description = "4782: Köp som företag(kort)-> leverera korttransaktion -> makulera korttransaktion")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void CreateOrderWithCardAsCompanyAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Company, PaymentMethods.Option.Card)

                    .RefreshPageUntil(x => 
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                        x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                        x.Orders.Count() > 0, 15, 3)

                    .Orders.Last().Order.OrderId.StoreValue(out var orderId)

                    // Validate order info
                    .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Open))
                    .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Card))

                    // Validate order rows info
                    .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(false.ToString())
                    .Orders.Last().OrderRows.First().Name.Should.Equal(products.First().Name)

                    // Validate deliveries info
                    .Orders.Last().Deliveries.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.True);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.CompanyEmail));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Card)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Open)));

                CollectionAssert.AreEquivalent(
                    new string[] { OrderActionType.CanDeliverOrder, OrderActionType.CanCancelOrder, OrderActionType.CanCancelAmount },
                    response.AvailableActions
                );
                Assert.That(response.OrderRows.Count, Is.EqualTo(1));
                Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
                Assert.That(response.Deliveries, Is.Null);
            });
        }

        [RetryWithException(2)]
        [Test(Description = "4782: Köp som företag(kort)-> leverera korttransaktion -> makulera korttransaktion")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void DeliverWithCardAsCompanyAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Company, PaymentMethods.Option.Card)

                    .RefreshPageUntil(x =>
                        x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                        x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                        x.Orders.Count() > 0, 15, 3)

                    // Deliver
                    .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                    .Orders.Last().Order.Table.Toggle.Click()
                    .Orders.Last().Order.Table.DeliverOrder.ClickAndGo()

                    // Validate order info
                    .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Delivered))
                    .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Card))

                    // Validate order rows info
                    .Orders.Last().OrderRows.Should.HaveCount(0)

                    // Validate deliveries info
                    .Orders.Last().Deliveries.First().Status.Should.BeNullOrEmpty();

            // Assert sdk/api response
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.True);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.CompanyEmail));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Card)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Delivered)));

                CollectionAssert.AreEquivalent(
                    new string[] { OrderActionType.CanCancelOrder, OrderActionType.CanCancelAmount },
                    response.AvailableActions
                );
                Assert.That(response.OrderRows, Is.Null);

                Assert.That(response.Deliveries.Count, Is.EqualTo(1));
                Assert.That(response.Deliveries.First().DeliveryAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
                Assert.That(response.Deliveries.First().CreditedAmount.InLowestMonetaryUnit, Is.EqualTo(0));
                Assert.That(response.Deliveries.First().Status, Is.Null);
                Assert.That(response.Deliveries.First().AvailableActions, Is.Empty);
            });
        }

        [RetryWithException(2)]
        [Test(Description = "4772: Köp som privatperson i anonyma flödet(kort) -> makulera transaktion")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void CreditWithCardAsPrivateAnonymousAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Anonymous, Entity.Option.Private, PaymentMethods.Option.Card)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                    x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                    x.Orders.Count() > 0, 15, 3)

                // Deliver -> Cancel
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.DeliverOrder.ClickAndGo()
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.CancelOrder.ClickAndGo()

                // Validate order info
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Cancelled))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Card))

                // Validate order rows info
                .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(false.ToString())
                .Orders.Last().OrderRows.First().Name.Should.Equal(products.First().Name)

                // Validate deliveries info
                .Orders.Last().Deliveries.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.False);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo("aaa@bbb.ccc"));
                Assert.That(response.OrderAmount.ToString("0,00"), Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice).ToString("0,00")));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Card)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Cancelled)));

                Assert.That(response.AvailableActions, Is.Empty);
                Assert.That(response.OrderRows.Count, Is.EqualTo(1));
                Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
                Assert.That(response.Deliveries, Is.Null);
            });
        }

        [RetryWithException(2)]
        [Test(Description = "4771: Köp som företag i anonyma flödet(kort) -> makulera transaktion")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void CreditWithCardAsCompanyAnonymousAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Anonymous, Entity.Option.Company, PaymentMethods.Option.Card)

                    .RefreshPageUntil(x =>
                        x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                        x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                        x.Orders.Count() > 0, 15, 3)

                    // Deliver -> Cancel
                    .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                    .Orders.Last().Order.Table.Toggle.Click()
                    .Orders.Last().Order.Table.DeliverOrder.ClickAndGo()
                    .Orders.Last().Order.Table.Toggle.Click()
                    .Orders.Last().Order.Table.CancelOrder.ClickAndGo()

                    // Validate order info
                    .RefreshPageUntil(x => x.Orders.Last().Order.OrderStatus.Value == nameof(OrderStatus.Cancelled), 10, 3)
                    .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Cancelled))
                    .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Card))

                    // Validate order rows info
                    .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(false.ToString())
                    .Orders.Last().OrderRows.First().Name.Should.Equal(products.First().Name)

                    // Validate deliveries info
                    .Orders.Last().Deliveries.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.True);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Card)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Cancelled)));

                Assert.That(response.AvailableActions, Is.Empty);
                Assert.That(response.OrderRows.Count, Is.EqualTo(1));
                Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
                Assert.That(response.Deliveries, Is.Null);
            });
        }

        [RetryWithException(2)]
        [Test(Description = "4782: Köp som företag(kort)-> leverera korttransaktion -> makulera korttransaktion")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void CancelWithCardAsCompanyAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Company, PaymentMethods.Option.Card)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                    x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                    x.Orders.Count() > 0, 15, 3)

                // Deliver -> Cancel
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.DeliverOrder.ClickAndGo()
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.CancelOrder.ClickAndGo()


                // Validate order info
                .RefreshPageUntil(x => x.Orders.Last().Order.OrderStatus.Value == nameof(OrderStatus.Cancelled), 10, 3)
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Cancelled))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Card))

                // Validate order rows info
                .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(false.ToString())
                .Orders.Last().OrderRows.First().Name.Should.Equal(products.First().Name)

                // Validate deliveries info
                .Orders.Last().Deliveries.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.True);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.CompanyEmail));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Card)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Cancelled)));

                Assert.That(response.AvailableActions, Is.Empty);
                Assert.That(response.OrderRows.Count, Is.EqualTo(1));
                Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
                Assert.That(response.Deliveries, Is.Null);
            });
        }

        [RetryWithException(2)]
        [Test(Description = "4777: Köp som privatperson(kort)-> leverera korttransaktion -> makulera korttransaktion")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void CancelWithCardAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Card)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                    x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                    x.Orders.Count() > 0, 15, 3)

                // Deliver -> Cancel
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.DeliverOrder.ClickAndGo()
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.CancelOrder.ClickAndGo()

                // Validate order info
                .RefreshPageUntil(x => x.Orders.Last().Order.OrderStatus.Value == nameof(OrderStatus.Cancelled), 10, 3)
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Cancelled))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Card))

                // Validate order rows info
                .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(false.ToString())
                .Orders.Last().OrderRows.First().Name.Should.Equal(products.First().Name)

                // Validate deliveries info
                .Orders.Last().Deliveries.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.False);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Card)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Cancelled)));

                Assert.That(response.AvailableActions, Is.Empty);
                Assert.That(response.OrderRows.Count, Is.EqualTo(1));
                Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
                Assert.That(response.Deliveries, Is.Null);
            });
        }

        [RetryWithException(2)]
        [Test(Description = "4782: Köp som privat(kort) med DiscountAmount-> leverera korttransaktion")]
        [TestCaseSource(nameof(TestData), new object[] { true, true, false, false })]
        public void CreateOrderWithDiscountAmountWithCardAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Company, PaymentMethods.Option.Card)

                   .RefreshPageUntil(x =>
                        x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                        x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                        x.Orders.Count() > 0, 15, 3)

                   .Orders.Last().Order.OrderId.StoreValue(out var orderId)

                   // Validate order info
                   .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Open))
                   .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Card))

                   // Validate order rows info
                   .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(false.ToString())
                   .Orders.Last().OrderRows.First().Name.Should.Equal(products.First().Name)

                   // Validate deliveries info
                   .Orders.Last().Deliveries.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.True);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.CompanyEmail));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Card)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Open)));

                CollectionAssert.AreEquivalent(
                    new string[] { OrderActionType.CanDeliverOrder, OrderActionType.CanCancelOrder, OrderActionType.CanCancelAmount },
                    response.AvailableActions
                );
                Assert.That(response.OrderRows.Count, Is.EqualTo(1));
                Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
                Assert.That(response.Deliveries, Is.Null);
            });
        }
    }
}
