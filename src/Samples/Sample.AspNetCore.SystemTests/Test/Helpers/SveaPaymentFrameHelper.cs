using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels;
using Sample.AspNetCore.SystemTests.Services;

namespace Sample.AspNetCore.SystemTests.Test.Helpers
{
    public static class SveaPaymentFrameHelper
    {
        public static SveaPaymentFramePage IdentifyEntity(this SveaPaymentFramePage page, Checkout.Option checkout = Checkout.Option.Identification, Entity.Option entity = Entity.Option.Private, PaymentMethods.Option paymentMethod = PaymentMethods.Option.Card, bool enableShipping = false)
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
                                return page.ProceedWithSwedishPrivateIdentification().Do(x =>
                                {
                                    if (enableShipping)
                                    {
                                        x.AddShippingBlock.IsVisible.WaitTo.BeTrue();
                                    }
                                    else
                                    {
                                        x.PaymentMethods.IsVisible.WaitTo.BeTrue();
                                    }
                                });
                            }
                            else
                            {
                                return page.ProceedWithNorwegianPrivateIdentification().Do(x =>
                                {
                                    if (enableShipping)
                                    {
                                        x.AddShippingBlock.IsVisible.WaitTo.BeTrue();
                                    }
                                    else
                                    {
                                        x.PaymentMethods.IsVisible.WaitTo.BeTrue();
                                    }
                                });
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

        public static SveaPaymentFramePage EditShipping(this SveaPaymentFramePage page)
        {
            return page.AddShippingBlock.IsVisible.WaitTo.BeTrue()
                .AddShippingBlock.Expand.Click()
                .EditShippingBlock.NewAddress.IsVisible.WaitTo.BeTrue()
                .EditShippingBlock.NewAddress.Click()
                .WaitSeconds(2)
                .EditShippingBlock.FirstName.Set(TestDataService.SwedishFirstName)
                .EditShippingBlock.LastName.Set(TestDataService.SwedishLastName)
                .EditShippingBlock.StreetAddress.Set(TestDataService.ShippingStreetAddress)
                .EditShippingBlock.ZipCode.Set(TestDataService.ShippingZipCode)
                .Do(x =>
                {
                    if(x.EditShippingBlock.City.Value.Length == 0)
                    {
                        x.EditShippingBlock.City.Set(TestDataService.ShippingCity);
                    }
                })
                .EditShippingBlock.Submit.Focus()
                .EditShippingBlock.Submit.Click()
                .SelectShippingBlock.Option.Items.ElementAt(2).Click()
                .WaitSeconds(2)
                .SelectShippingBlock.DoorCode.Set(TestDataService.SwedishZipCode)
                .SelectShippingBlock.Instructions.Set("Test")
                .WaitSeconds(2)
                .SelectShippingBlock.Submit.Focus()
                .SelectShippingBlock.Submit.Click();
        }

        public static SveaPaymentFramePage Pay(this SveaPaymentFramePage page, Checkout.Option checkout = Checkout.Option.Identification, Entity.Option entity = Entity.Option.Private, PaymentMethods.Option paymentMethod = PaymentMethods.Option.Card, string amount = null)
        {
            switch (paymentMethod)
            {
                default:
                case PaymentMethods.Option.Card:
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
            }

            //page
            //    .PaymentMethods.TotalAmount.IsVisible.WaitTo.BeTrue()
            //    .PaymentMethods.TotalAmount.Should.ContainAmount(amount);

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

                case PaymentMethods.Option.Vipps:
                    return page.PayWithVipps();
            }
        }
    }
}
