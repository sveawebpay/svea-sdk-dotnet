using Atata;
using NUnit.Framework;
using Sample.AspNetCore.SystemTests.Services;
using Sample.AspNetCore.SystemTests.Test.Base;
using Sample.AspNetCore.SystemTests.Test.Helpers;
using Svea.WebPay.SDK.CheckoutApi;
using System;
using System.Linq;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Payment
{
    public class LeasingOrderTests : Base.PaymentTests
    {
        public LeasingOrderTests(string driverAlias)
            : base(driverAlias)
        {
        }

        [RetryWithException(2)]
        [Test(Description = "")]
        [TestCaseSource(nameof(TestData), new object[] { false, false, false, true })]
        public void CreditWithVippsAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, Helpers.Checkout.Option.Identification, Entity.Option.Company, PaymentMethods.Option.Leasing)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                    x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                    x.Orders.Count() > 0, 15, 3);

                // Assert sdk/api response
                var response = await _sveaClientSweden.Checkout.GetOrder(long.Parse(_orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.PaymentType, Is.EqualTo(PaymentType.LEASINGMANUAL));
                Assert.That(response.Payment.PaymentMethodType, Is.EqualTo(PaymentMethodType.Leasing));
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.CompanyEmail));
                Assert.That(response.Status, Is.EqualTo(CheckoutOrderStatus.Final));
            });
        }
    }
}
