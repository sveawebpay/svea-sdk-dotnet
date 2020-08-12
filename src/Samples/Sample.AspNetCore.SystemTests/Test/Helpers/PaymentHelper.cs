using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels;
using Sample.AspNetCore.SystemTests.PageObjectModels.Payment;
using Sample.AspNetCore.SystemTests.Services;
using System;

namespace Sample.AspNetCore.SystemTests.Test.Helpers
{
    public static class PaymentHelper
    {
        public static SveaPaymentFramePage PayWithCard(this SveaPaymentFramePage page)
        {
            return page
            .PaymentMethods.Card.IsVisible.WaitTo.BeTrue()
            .PaymentMethods.Card.Click()
            .Submit.ClickAndGo<CardPaymentPage>()
            .CardNumber.IsVisible.WaitTo.BeTrue()
            .Do(x =>
            {
                if (x.DebitCard.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }))
                {
                    x.DebitCard.Click();
                }
            })
            .CardNumber.Set(TestDataService.CreditCardNumber)
            .Expiry.Set(TestDataService.CreditCardExpiratioDate)
            .Cvc.Set(TestDataService.CreditCardCvc)
            .Submit.Click()
            .SwitchToRoot<SveaPaymentFramePage>();
        }

        public static SveaPaymentFramePage PayWithDirektBank(this SveaPaymentFramePage page)
        {
            return page
                .PaymentMethods.DirektBank.IsVisible.WaitTo.BeTrue()
                .PaymentMethods.DirektBank.Click()
                .PaymentMethods.DirektBank.Nordea.Click()
                .Submit.Click();
        }

        public static SveaPaymentFramePage PayWithInvoice(this SveaPaymentFramePage page)
        {
            return page
                .PaymentMethods.Invoice.IsVisible.WaitTo.BeTrue()
                .PaymentMethods.Invoice.Click()
                .Submit.Click();
        }

        public static SveaPaymentFramePage PayWithTrustly(this SveaPaymentFramePage page, Checkout.Option checkout)
        {
            return page
                .PaymentMethods.Trustly.IsVisible.WaitTo.BeTrue()
                .PaymentMethods.Trustly.Click()
                .Submit.ClickAndGo<TrustlyPaymentPage>()
                .Banks[0].IsVisible.WaitTo.BeTrue()
                .Banks[0].Click()
                .Next.Click()
                .Do(x => { 
                    if(checkout == Checkout.Option.Anonymous)
                    {
                        x.PersonalNumber.Set(TestDataService.PersonalNumber);
                    }
                })
                .SecurityCodeOption.Click()
                .Next.Click()
                .MessageCode.StoreValue(out string code)
                .Code.Set(code)
                .Next.Click()
                .AccountOptions.IsVisible.WaitTo.Within(60).BeTrue()
                .Next.Click()
                .MessageCode.StoreValue(out code)
                .Code.Set(code)
                .Next.Click()
                .SwitchToRoot<SveaPaymentFramePage>();
        }

        public static SveaPaymentFramePage PayWithPaymentPlan(this SveaPaymentFramePage page)
        {
            return page
                .PaymentMethods.PaymentPlan.IsVisible.WaitTo.BeTrue()
                .PaymentMethods.PaymentPlan.Click()
                .PaymentMethods.PaymentPlan.Options[1].Click()
                .Submit.Click();
        }

        public static SveaPaymentFramePage PayWithAccount(this SveaPaymentFramePage page)
        {
            return page
                .PaymentMethods.Account.IsVisible.WaitTo.BeTrue()
                .PaymentMethods.Account.Click()
                .Submit.Click();
        }

        public static SveaPaymentFramePage PayWithBlackFriday(this SveaPaymentFramePage page)
        {
            return page
                .PaymentMethods.BlackFriday.IsVisible.WaitTo.BeTrue()
                .PaymentMethods.BlackFriday.Click()
                .Submit.Click();
        }

        public static SveaPaymentFramePage PayWithSwish(this SveaPaymentFramePage page)
        {
            return page
                .PaymentMethods.Swish.IsVisible.WaitTo.BeTrue()
                .PaymentMethods.Swish.Click()
                .Submit.Click();
        }
    }
}