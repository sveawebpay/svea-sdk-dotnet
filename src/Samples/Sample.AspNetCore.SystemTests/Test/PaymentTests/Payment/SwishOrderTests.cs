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
    public class SwishOrderTests : Base.PaymentTests
    {
        public SwishOrderTests(string driverAlias)
            : base(driverAlias)
        {
        }

        [RetryWithException(2)]
        [Test(Description = "4780: Köp som privatperson(Swish, om vi kan få till det på en merchant) -> kreditera transaktion")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void CreditWithSwishAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Swish)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                    x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                    x.Orders.Count() > 0, 15, 3)

                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Deliveries.First().Table.Toggle.Click()
                .Orders.Last().Deliveries.First().Table.CreditAmount.ClickAndGo()

                // Validate order info
                .RefreshPageUntil(x => x.Orders.Last().Order.OrderStatus.Value == nameof(OrderStatus.Delivered), 10, 3)
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Delivered))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Swish))
                .Orders.Last().Order.Table.Toggle.Click();

            // Assert sdk/api response
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.False);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Swish)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Delivered)));
                Assert.That(response.AvailableActions.Count, Is.EqualTo(0));

                Assert.Null(response.OrderRows);

                Assert.That(response.Deliveries.Count, Is.EqualTo(1));
                CollectionAssert.AreEquivalent(
                    new string[] { DeliveryActionType.CanCreditAmount },
                    response.Deliveries.First().AvailableActions
                );
            });
        }

    }
}
