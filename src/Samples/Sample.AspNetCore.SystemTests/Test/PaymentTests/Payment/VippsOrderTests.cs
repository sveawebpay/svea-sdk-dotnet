using Atata;
using NUnit.Framework;
using Sample.AspNetCore.SystemTests.Services;
using Sample.AspNetCore.SystemTests.Test.Base;
using Sample.AspNetCore.SystemTests.Test.Helpers;
using Svea.WebPay.SDK.PaymentAdminApi;
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
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Vipps);

                // Assert sdk/api response
                var response = await _sveaClientNorway.PaymentAdmin.GetOrder(long.Parse(_orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("NOK"));
                Assert.That(response.IsCompany, Is.False);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Vipps)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Open)));
                CollectionAssert.AreEquivalent(
                    new string[] { OrderActionType.CanDeliverOrder, OrderActionType.CanCancelOrder, OrderActionType.CanCancelAmount },
                    response.AvailableActions
                );

                Assert.That(response.OrderRows.Count(), Is.EqualTo(1));
                Assert.That(response.OrderRows.First().AvailableActions.Count, Is.EqualTo(0));

                Assert.IsNull(response.Deliveries);
            });
        }

    }
}
