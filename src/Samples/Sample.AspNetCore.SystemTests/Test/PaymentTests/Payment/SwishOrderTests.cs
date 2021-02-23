using Atata;
using NUnit.Framework;
using Sample.AspNetCore.SystemTests.Services;
using Sample.AspNetCore.SystemTests.Test.Base;
using Sample.AspNetCore.SystemTests.Test.Helpers;
using Svea.WebPay.SDK.PaymentAdminApi;
using System.Linq;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Payment
{
    public class SwishOrderTests : Base.PaymentTests
    {
        public SwishOrderTests(string driverAlias)
            : base(driverAlias)
        {
        }

        [RetryWithException(3)]
        [Test(Description = "4780: Köp som privatperson(Swish, om vi kan få till det på en merchant) -> kreditera transaktion")]
        [TestCaseSource(nameof(TestData), new object[] { true, false })]
        public async System.Threading.Tasks.Task CreditWithSwishAsPrivateAsync(Product[] products)
        {
            GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Swish)
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Deliveries.First().Table.Toggle.Click()
                .Orders.Last().Deliveries.First().Table.CreditAmount.ClickAndGo()

                // Validate order info
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Delivered))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Swish))
                .Orders.Last().Order.Table.Toggle.Click();

            // Assert sdk/api response
            var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

            Assert.That(response.Currency, Is.EqualTo("SEK"));
            Assert.That(response.IsCompany, Is.False);
            Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
            Assert.That(response.OrderAmount, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Swish)));
            Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Delivered)));
            Assert.That(response.AvailableActions.Count, Is.EqualTo(0));

            Assert.Null(response.OrderRows);

            Assert.That(response.Deliveries.Count, Is.EqualTo(1));
            CollectionAssert.AreEquivalent(
                new string[] { DeliveryActionType.CanCreditAmount },
                response.Deliveries.First().AvailableActions
            );
        }

    }
}
