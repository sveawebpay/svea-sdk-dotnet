using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels;
using Sample.AspNetCore.SystemTests.Services;

namespace Sample.AspNetCore.SystemTests.Test.Helpers
{
    public static class SveaPaymentFrameHelper
    {
        public static SveaPaymentFramePage IdentifyEntity(this SveaPaymentFramePage page, Checkout.Option checkout = Checkout.Option.Identification, Entity.Option entity = Entity.Option.Private, PaymentMethods.Option paymentMethod = PaymentMethods.Option.Card)
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
                            
                            if(paymentMethod != PaymentMethods.Option.Vipps)
                            {
                                return page.ProceedWithSwedishPrivateIdentification().PaymentMethods.IsVisible.WaitTo.BeTrue();
                            }
                            else if(paymentMethod == PaymentMethods.Option.MobilePay)
                            {
                                return page.ProceedWithFinnishPrivateIdentification().PaymentMethods.IsVisible.WaitTo.BeTrue();
                            }
                            else
                            {
                                return page.ProceedWithNorwegianPrivateIdentification().PaymentMethods.IsVisible.WaitTo.BeTrue();
                            }

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
            switch (paymentMethod)
            {
                default:
                case PaymentMethods.Option.Card:
                case PaymentMethods.Option.CardEmbedded:
                    page.PaymentMethods.Card.IsVisible.WaitTo.BeTrue().PaymentMethods.Card.Click();
                    break;

                case PaymentMethods.Option.DirektBank:
                    page.PaymentMethods.DirektBank.IsVisible.WaitTo.BeTrue().PaymentMethods.DirektBank.Click();
                    break;

                case PaymentMethods.Option.Invoice:
                    page.PaymentMethods.Invoice.IsVisible.WaitTo.BeTrue().PaymentMethods.Invoice.Click();
                    break;

                case PaymentMethods.Option.Trustly:
                    page.PaymentMethods.Trustly.IsVisible.WaitTo.BeTrue().PaymentMethods.Trustly.Click();
                    break;

                case PaymentMethods.Option.PaymentPlan:
                    page.PaymentMethods.PaymentPlan.IsVisible.WaitTo.BeTrue().PaymentMethods.PaymentPlan.Click();
                    break;

                case PaymentMethods.Option.AccountCredit:
                    page.PaymentMethods.Account.IsVisible.WaitTo.BeTrue().PaymentMethods.Account.Click();
                    break;

                case PaymentMethods.Option.BlackFriday:
                    page.PaymentMethods.BlackFriday.IsVisible.WaitTo.BeTrue().PaymentMethods.BlackFriday.Click();
                    break;

                case PaymentMethods.Option.Swish:
                    page.PaymentMethods.Swish.IsVisible.WaitTo.BeTrue().PaymentMethods.Swish.Click();
                    break;

                case PaymentMethods.Option.Vipps:
                    page.PaymentMethods.Vipps.IsVisible.WaitTo.BeTrue().PaymentMethods.Vipps.Click();
                    break;

                case PaymentMethods.Option.Leasing:
                    page.PaymentMethods.Leasing.IsVisible.WaitTo.BeTrue().PaymentMethods.Leasing.Click();
                    break;

                case PaymentMethods.Option.MobilePay:
                    page.PaymentMethods.MobilePay.IsVisible.WaitTo.BeTrue().PaymentMethods.MobilePay.Click();
                    break;
            }

            //page
            //    .PaymentMethods.TotalAmount.IsVisible.WaitTo.BeTrue()
            //    .PaymentMethods.TotalAmount.Should.ContainAmount(amount);

            if (entity == Entity.Option.Company && checkout == Checkout.Option.Identification)
            {
                page.WaitSeconds(1)
                    .PaymentMethods.Reference.Set(TestDataService.Reference);
            }

            switch (paymentMethod)
            {
                default:
                case PaymentMethods.Option.Card:
                    return page.PayWithCard();

                case PaymentMethods.Option.CardEmbedded:
                    return page.PayWithCardEmbedded();

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

                case PaymentMethods.Option.Vipps:
                    return page.PayWithVipps();

                case PaymentMethods.Option.Leasing:
                    return page.PayWithLeasing();

                case PaymentMethods.Option.MobilePay:
                    return page.PayWithMobilePay();
            }
        }
    }
}
