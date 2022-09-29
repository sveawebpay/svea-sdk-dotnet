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
    public class PaymentPlanOrderTests : Base.PaymentTests
    {
        public PaymentPlanOrderTests(string driverAlias)
            : base(driverAlias)
        {
        }

        [RetryWithException(2)]
        [Test(Description = "4779: Köp som privatperson(delbetala) -> leverera delbetalning -> kreditera delbetalning")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void CreateOrderWithPaymentPlanAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.PaymentPlan)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                    x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                    x.Orders.Count() > 0, 15, 3)

                .Orders.Last().Order.OrderId.StoreValue(out var orderId)

                // Validate order info
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Open))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.PaymentPlan))

                // Validate order row info
                .Orders.Last().OrderRows.Count.Should.Equal(1)
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
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.PaymentPlan)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Open)));

                CollectionAssert.AreEquivalent(
                    new string[] { OrderActionType.CanDeliverOrder, OrderActionType.CanCancelOrder, OrderActionType.CanAddOrderRow, OrderActionType.CanCancelOrderRow, OrderActionType.CanUpdateOrderRow },
                    response.AvailableActions
                );
                Assert.That(response.OrderRows.Count, Is.EqualTo(1));
                CollectionAssert.AreEquivalent(
                    new string[] { OrderRowActionType.CanCancelRow, OrderRowActionType.CanUpdateRow },
                    response.OrderRows.First().AvailableActions
                );

                Assert.That(response.Deliveries, Is.Null);
            });
        }

        [RetryWithException(2)]
        [Test(Description = "4779: Köp som privatperson(delbetala) -> leverera delbetalning -> kreditera delbetalning")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void DeliverWithPaymentPlanAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.PaymentPlan)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                    x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                    x.Orders.Count() > 0, 15, 3)

                // Deliver
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.DeliverOrder.ClickAndGo()

                // Validate order info
                .RefreshPageUntil(x => x.Orders.Last().Order.OrderStatus.Value == nameof(OrderStatus.Delivered), 10, 3)
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Delivered))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.PaymentPlan))

                // Validate order row info
                .Orders.Last().OrderRows.Should.HaveCount(0)

                // Validate deliveries info
                .Orders.Last().Deliveries.Count.Should.Equal(1)
                .Orders.Last().Deliveries.First().Status.Should.BeNullOrEmpty();

            // Assert sdk/api response
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.False);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.PaymentPlan)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Delivered)));

                Assert.That(response.AvailableActions, Is.Empty);
                Assert.That(response.OrderRows, Is.Empty);

                Assert.That(response.Deliveries.Count, Is.EqualTo(1));
                Assert.That(response.Deliveries.First().DeliveryAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
                Assert.That(response.Deliveries.First().CreditedAmount.InLowestMonetaryUnit, Is.EqualTo(0));
                Assert.That(response.Deliveries.First().Status, Is.Null);
                CollectionAssert.AreEquivalent(
                    new string[] { DeliveryActionType.CanCreditNewRow, DeliveryActionType.CanCreditOrderRows },
                    response.Deliveries.First().AvailableActions
                );
            });
        }

        [RetryWithException(2)]
        [Test(Description = "4779: Köp som privatperson(delbetala) -> leverera delbetalning -> kreditera delbetalning")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void CreditWithPaymentPlanAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.PaymentPlan)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                    x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                    x.Orders.Count() > 0, 15, 3)

                // Deliver -> Credit
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.DeliverOrder.ClickAndGo()
                .Orders.Last().Deliveries.First().Table.Toggle.Click()
                .Orders.Last().Deliveries.First().Table.CreditOrderRows.ClickAndGo()

                // Validate order info
                .RefreshPageUntil(x => x.Orders.Last().Order.OrderStatus.Value == nameof(OrderStatus.Cancelled), 10, 3)
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Cancelled))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.PaymentPlan))

                // Validate order rows info
                .Orders.Last().OrderRows.Should.HaveCount(0)

                // Validate deliveries info
                .Orders.Last().Deliveries.Count.Should.Equal(1)
                .Orders.Last().Deliveries.First().Status.Should.BeNullOrEmpty();

            // Assert sdk/api response
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.False);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.PaymentPlan)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Cancelled)));

                Assert.That(response.AvailableActions, Is.Empty);
                Assert.That(response.OrderRows, Is.Empty);

                Assert.That(response.Deliveries.Count, Is.EqualTo(1));
                Assert.That(response.Deliveries.First().DeliveryAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
                Assert.That(response.Deliveries.First().CreditedAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
                Assert.That(response.Deliveries.First().Credits.Count, Is.EqualTo(1));
                Assert.That(response.Deliveries.First().Status, Is.Null);

                Assert.That(response.Deliveries.First().AvailableActions, Is.Empty);
            });
        }

        [RetryWithException(2)]
        [Test(Description = "4778: Köp som privatperson(delbetala) -> makulera delbetalning")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void CancelWithPaymentPlanAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.PaymentPlan)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                    x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                    x.Orders.Count() > 0, 15, 3)

                // Cancel
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.CancelOrder.ClickAndGo()

                // Validate order info
                .RefreshPageUntil(x => x.Orders.Last().Order.OrderStatus.Value == nameof(OrderStatus.Cancelled), 10, 3)
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Cancelled))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.PaymentPlan))

                // Validate order rows info
                .Orders.Last().OrderRows.Count.Should.Equal(1)
                .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(true.ToString())
                .Orders.Last().OrderRows.First().Name.Should.Equal(products.First().Name)

                // Validate deliveries info
                .Orders.Last().Deliveries.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.False);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.CancelledAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.PaymentPlan)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Cancelled)));

                Assert.That(response.AvailableActions, Is.Empty);
                Assert.That(response.OrderRows.Count, Is.EqualTo(1));
                Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
                Assert.That(response.OrderRows.First().IsCancelled, Is.True);
                Assert.That(response.Deliveries, Is.Null);
            });
            
        }

        [RetryWithException(2)]
        [Test(Description = "5702: RequireElectronicIdAuthentication] As a user I want to have a setting that will trigger BankId to be required on orders in the checkout")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void EnsureRequireIdAuthenticationShowUpWithPaymentPlan(Product[] products)
        {
            Assert.DoesNotThrow(() => 
            { 
                GoToBankId(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.PaymentPlan); 
            });
           
        }
    }
}
