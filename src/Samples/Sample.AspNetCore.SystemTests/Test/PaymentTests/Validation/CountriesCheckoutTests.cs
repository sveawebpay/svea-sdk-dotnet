using Atata;
using NUnit.Framework;
using Sample.AspNetCore.SystemTests.PageObjectModels;
using Sample.AspNetCore.SystemTests.PageObjectModels.Payment;
using Sample.AspNetCore.SystemTests.PageObjectModels.ThankYou;
using Sample.AspNetCore.SystemTests.Services;
using Sample.AspNetCore.SystemTests.Test.Base;
using Sample.AspNetCore.SystemTests.Test.Helpers;
using System;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Payment
{
    public class CountriesCheckoutTests : Base.PaymentTests
    {
        public CountriesCheckoutTests(string driverAlias)
            : base(driverAlias)
        {
        }

        [RetryWithException(2)]
        [Test(Description = "5704: [International flow] As a user I want to be able to choose different parameters as language/country/market")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void ChangeCountryAtCheckout(Product[] products)
        {
            Assert.DoesNotThrow(() => 
            {
                SelectProducts(products, PaymentMethods.Option.CardEmbedded)
                    .Country.Click()
                    .Countries[m => m.Content.Value == "NO"].Click()
                    .Market.Click()
                    .Markets[m => m.Content.Value == "SE"].Click()
                    .InternationalCheckout.ClickAndGo()
                    .SveaFrame.SwitchTo<SveaPaymentFramePage>()
                    .International.Should.WithinSeconds(20).BeVisible()
                    .International.Country.Should.Contain("Norway")
                    .SwitchToRoot<PaymentPage>()
                    .Country.Click()
                    .Countries[m => m.Content.Value == "BE"].Click()
                    .SveaFrame.SwitchTo<SveaPaymentFramePage>()
                    .International.Should.WithinSeconds(20).BeVisible()
                    .International.Country.Should.Contain("Belgium")
                    .SwitchToRoot<PaymentPage>()
                    .Country.Click()
                    .Countries[m => m.Content.Value == "FR"].Click()
                    .SveaFrame.SwitchTo<SveaPaymentFramePage>()
                    .International.Should.WithinSeconds(20).BeVisible()
                    .International.Country.Should.Contain("France")
                    .SwitchToRoot<PaymentPage>()
                    .Country.Click()
                    .Countries[m => m.Content.Value == "LT"].Click()
                    .SveaFrame.SwitchTo<SveaPaymentFramePage>()
                    .International.Should.WithinSeconds(20).BeVisible()
                    .International.Country.Should.Contain("Lithuania")
                    .International.FirstName.Set(TestDataService.SwedishFirstName)
                    .International.LastName.Set(TestDataService.SwedishLastName)
                    .International.Email.Set(TestDataService.Email)
                    .International.PhoneNumber.Set(TestDataService.SwedishPhoneNumber)
                    .International.Street.Set(TestDataService.SwedishStreet)
                    .International.ZipCode.Set(TestDataService.SwedishZipCode)
                    .International.City.Set(TestDataService.SwedishCity)
                    .Submit.Click()
                    .Pay(Checkout.Option.Anonymous, Entity.Option.Private, PaymentMethods.Option.CardEmbedded, null)
                    .PageUrl.Should.Within(TimeSpan.FromSeconds(60)).Contain("thankyou")
                    .SwitchToRoot<ThankYouPage>()
                    .ThankYou.IsVisible.WaitTo.BeTrue();
            });
        }
    }
}
