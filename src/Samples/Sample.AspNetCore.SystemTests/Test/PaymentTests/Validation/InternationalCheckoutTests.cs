using Atata;
using NUnit.Framework;
using Sample.AspNetCore.SystemTests.PageObjectModels;
using Sample.AspNetCore.SystemTests.Test.Base;
using Sample.AspNetCore.SystemTests.Test.Helpers;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Payment
{
    public class InternationalCheckoutTests : Base.PaymentTests
    {
        public InternationalCheckoutTests(string driverAlias)
            : base(driverAlias)
        {
        }

        [RetryWithException(3)]
        [Test(Description = "5704: [International flow] As a user I want to be able to choose US as language/market, but SEK as currency and by that trigger international flow in the checkout")]
        [TestCaseSource(nameof(TestData), new object[] { true, false })]
        public void CreateOrderWithCardAsCompanyAsync(Product[] products)
        {
            SelectProducts(products)
                .InternationalCheckout.ClickAndGo()
                .SveaFrame.SwitchTo<SveaPaymentFramePage>()
                .International.Should.Within(20).BeVisible()
                .International.Country.Should.Contain("United States of America")
                .Entity.Should.Not.BeVisible()
                .SwitchToRoot<ProductsPage>()
                .Header.Products.ClickAndGo<ProductsPage>()
                .AnonymousCheckout.ClickAndGo()
                .SveaFrame.SwitchTo<SveaPaymentFramePage>()
                .Entity.Should.Within(20).BeVisible()
                .International.Should.Not.BeVisible();
        }
    }
}
