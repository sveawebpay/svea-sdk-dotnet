using NUnit.Framework;
using Sample.AspNetCore.SystemTests.Test.Base;
using Sample.AspNetCore.SystemTests.Test.Helpers;
using Svea.WebPay.SDK.CheckoutApi;
using System;
using System.Threading;

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
        public void CreditWithLeasingAsCompanyAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                GoToThankYouPage(products, Helpers.Checkout.Option.Identification, Entity.Option.Company, PaymentMethods.Option.Leasing);

                var count = 0;
                Data response = null;

                while (true && count < 5)
                {
                    try
                    {
                        // Assert sdk/api response
                        response = await _sveaClientSweden.Checkout.GetOrder(long.Parse(_orderId)).ConfigureAwait(false);
                        Assert.NotNull(response);
                        break;

                    }
                    catch (Exception ex)
                    {
                        count++;
                        Thread.Sleep(1000 * 3);
                    }
                }

                // Verification not available yet

                //Assert.That(response.Currency, Is.EqualTo("SEK"));
                //Assert.That(response.PaymentType, Is.EqualTo(PaymentType.LEASING));
                //Assert.That(response.Payment.PaymentMethodType, Is.EqualTo(PaymentMethodType.Leasing));
                //Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.CompanyEmail));
                //Assert.That(response.Status, Is.EqualTo(CheckoutOrderStatus.Final));
            });
        }
    }
}
