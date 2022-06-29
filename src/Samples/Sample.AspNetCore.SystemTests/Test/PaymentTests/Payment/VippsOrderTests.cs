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
    public class VippsOrderTests : Base.PaymentTests
    {
        public VippsOrderTests(string driverAlias)
            : base(driverAlias)
        {
        }

        [RetryWithException(2)]
        [Test(Description = "")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void CreditWithVippsAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Vipps)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                    x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                    x.Orders.Count() > 0, 15, 3)

                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().OrderRows.First().Table.Toggle.Click()
                .Orders.Last().OrderRows.First().Table.DeliverOrderRowQuantity.Set(1)
                .Orders.Last().OrderRows.First().Table.DeliverOrderRow.ClickAndGo()

                // Validate order info
                .RefreshPageUntil(x => x.Orders.Last().Order.OrderStatus.Value == nameof(OrderStatus.Delivered), 10, 3)
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Delivered))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Vipps))
                .Orders.Last().Order.Table.Toggle.Click();

                // Assert sdk/api response
                var response = await _sveaClientNorway.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("NOK"));
                Assert.That(response.IsCompany, Is.False);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Vipps)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Delivered)));
                CollectionAssert.AreEquivalent(
                    new string[] { OrderActionType.CanCancelOrder, OrderActionType.CanCancelAmount },
                    response.AvailableActions
                );

                Assert.Null(response.OrderRows);

                Assert.That(response.Deliveries.Count(), Is.EqualTo(1));
                Assert.That(response.Deliveries.First().AvailableActions.Count, Is.EqualTo(0));
            });
        }
    }
}
