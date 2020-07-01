using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels;
using Sample.AspNetCore.SystemTests.Services;

namespace Sample.AspNetCore.SystemTests.Test.Helpers
{
    public static class SveaPaymentFrameHelper
    {
        public static SveaPaymentFramePage IdentifyEntity(this SveaPaymentFramePage page, Checkout.Option checkout = Checkout.Option.Identification, Entity.Option entity = Entity.Option.Private)
        {
            page.Entity.IsVisible.WaitTo.BeTrue();

            switch (entity)
            {
                default:
                case Entity.Option.Private:

                    switch (checkout)
                    {
                        default:
                        case Checkout.Option.Identification:
                            return page.ProceedWithPrivateIdentification().PaymentMethods.IsVisible.WaitTo.BeTrue();

                        case Checkout.Option.Anonymous:
                            return page.ProceedWithPrivateAnonymous().PaymentMethods.IsVisible.WaitTo.BeTrue();
                    }

                case Entity.Option.Company:

                    switch (checkout)
                    {
                        default:
                        case Checkout.Option.Identification:
                            return page.ProceedWithCompanyIdentification().PaymentMethods.IsVisible.WaitTo.BeTrue();

                        case Checkout.Option.Anonymous:
                            return page.ProceedWithCompanyAnonymous().PaymentMethods.IsVisible.WaitTo.BeTrue();
                    }
            }
        }

        public static SveaPaymentFramePage Pay(this SveaPaymentFramePage page, Checkout.Option checkout = Checkout.Option.Identification, Entity.Option entity = Entity.Option.Private, PaymentMethods.Option paymentMethod = PaymentMethods.Option.Card, string amount = null)
        {
            page
            .PaymentMethods.TotalAmount.IsVisible.WaitTo.BeTrue()
            .PaymentMethods.TotalAmount.Should.ContainAmount(amount);

            if (entity == Entity.Option.Company && checkout == Checkout.Option.Identification)
            {
                page.PaymentMethods.Reference.Set(TestDataService.Reference);
            }

            switch (paymentMethod)
            {
                default:
                case PaymentMethods.Option.Card:
                    return page.PayWithCard();

                case PaymentMethods.Option.DirektBank:
                    return page.PayWithDirektBank();

                case PaymentMethods.Option.Invoice:
                    return page.PayWithInvoice();

                case PaymentMethods.Option.Trustly:
                    return page.PayWithTrustly(checkout);

                case PaymentMethods.Option.PaymentPlan:
                    return page.PayWithPaymentPlan();

                case PaymentMethods.Option.AccountCredit:
                    return page.PayWithAccount();

                case PaymentMethods.Option.BlackFriday:
                    return page.PayWithBlackFriday();

                case PaymentMethods.Option.Swish:
                    return page.PayWithSwish();
            }
        }
    }
}
