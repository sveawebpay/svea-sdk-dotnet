using Atata;
using NUnit.Framework;
using Sample.AspNetCore.SystemTests.Services;
using Sample.AspNetCore.SystemTests.Test.Base;
using Sample.AspNetCore.SystemTests.Test.Helpers;
using Svea.WebPay.SDK.CheckoutApi;
using Svea.WebPay.SDK.PaymentAdminApi;
using System;
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
        [Test(Description = "")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void MakeOrderWithShippingOption(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Card, true)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                        x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                        x.Orders.Count() > 0, 15, 3)

                    .Orders.Last().Order.OrderId.StoreValue(out var orderId)

                    // Validate order info
                    .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Open))
                    .Orders.Last().Order.PaymentType.Should.Equal(nameof(Svea.WebPay.SDK.PaymentAdminApi.PaymentType.Card))

                    // Validate deliveries info
                    .Orders.Last().Deliveries.Should.HaveCount(0);

                // Assert sdk/api response
                var response = await _sveaClientSweden.Checkout.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(Svea.WebPay.SDK.CheckoutApi.PaymentType.SVEACARDPAY)));
                Assert.That(response.Status.ToString(), Is.EqualTo(nameof(CheckoutOrderStatus.Final)));

                Assert.That(response.ShippingAddress.FirstName, Is.EqualTo(TestDataService.SwedishFirstName));
                Assert.That(response.ShippingAddress.LastName, Is.EqualTo(TestDataService.SwedishLastName));
                Assert.That(response.ShippingAddress.StreetAddress, Is.EqualTo(TestDataService.ShippingStreetAddress));
                Assert.That(response.ShippingAddress.PostalCode, Is.EqualTo(TestDataService.ShippingZipCode.Replace(" ", "")));
                Assert.That(response.ShippingAddress.City, Is.EqualTo(TestDataService.ShippingCity.ToUpper()));
            });
        }
    }
}
