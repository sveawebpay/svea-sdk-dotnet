using Atata;
using NUnit.Framework;
using Sample.AspNetCore.SystemTests.Test.Base;
using Sample.AspNetCore.SystemTests.Test.Helpers;
using System;
using System.Linq;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Payment
{
    public class MobilePayOrderTests : Base.PaymentTests
    {
        public MobilePayOrderTests(string driverAlias)
            : base(driverAlias)
        {
        }

        [Ignore("Testing MobilePay is not sorted out yet")]
        [RetryWithException(2)]
        [Test(Description = "")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void CreditWithVippsAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, Helpers.Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.MobilePay, requireBankId: true)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                    x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                    x.Orders.Count() > 0, 15, 3);
            });
        }
    }
}
