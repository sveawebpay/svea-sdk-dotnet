using Atata;
using OpenQA.Selenium;
using Sample.AspNetCore.SystemTests.PageObjectModels;
using Sample.AspNetCore.SystemTests.Services;
using System.Collections.Generic;
using System.Linq;

namespace Sample.AspNetCore.SystemTests.Test.Helpers
{
    public static class SveaPaymentFrameHelper
    {
        public static SveaPaymentFramePage IdentifyEntity(this SveaPaymentFramePage page, Checkout.Option checkout = Checkout.Option.Identification, Entity.Option entity = Entity.Option.Private, PaymentMethods.Option paymentMethod = PaymentMethods.Option.Card, Dictionary<string, string[]> shipping = null)
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
                                    if (shipping != null)
                                    {
                                        x.AddShippingBlock.IsVisible.WaitTo.WithinSeconds(30).BeTrue();
                                    }
                                    else
                                    {
                                        x.PaymentMethods.IsVisible.WaitTo.WithinSeconds(30).BeTrue();
                                    }
                                });
                            }
                            else if(paymentMethod == PaymentMethods.Option.MobilePay)
                            {
                                return page.ProceedWithFinnishPrivateIdentification().PaymentMethods.IsVisible.WaitTo.BeTrue();
                            }
                            else
                            {
                                return page.ProceedWithNorwegianPrivateIdentification().Do(x =>
                                {
                                    if (shipping != null)
                                    {
                                        x.AddShippingBlock.IsVisible.WaitTo.WithinSeconds(30).BeTrue();
                                    }
                                    else
                                    {
                                        x.PaymentMethods.IsVisible.WaitTo.WithinSeconds(30).BeTrue();
                                    }
                                });
                            }

                        case Checkout.Option.Anonymous:
                            return page.ProceedWithPrivateAnonymous().PaymentMethods.IsVisible.WaitTo.WithinSeconds(30).BeTrue();
                    }

                case Entity.Option.Company:

                    switch (checkout)
                    {
                        default:
                        case Checkout.Option.Identification:
                            if(paymentMethod == PaymentMethods.Option.Leasing)
                            {
                                return page.ProceedWithCompanyIdentification(TestDataService.OrganizationNumberLeasing).PaymentMethods.IsVisible.WaitTo.WithinSeconds(30).BeTrue();
                            }
                            else
                            {
                                return page.ProceedWithCompanyIdentification(TestDataService.OrganizationNumber).PaymentMethods.IsVisible.WaitTo.WithinSeconds(30).BeTrue();
                            }

                        case Checkout.Option.Anonymous:
                            return page.ProceedWithCompanyAnonymous().PaymentMethods.IsVisible.WaitTo.WithinSeconds(30).BeTrue();
                    }
            }
        }

        public static SveaPaymentFramePage EditShipping(this SveaPaymentFramePage page, Dictionary<string, string[]> shipping)
        {
            return page.AddShippingBlock.IsVisible.WaitTo.BeTrue()
                .AddShippingBlock.Expand.Click()
                .EditShippingBlock.NewAddress.IsVisible.WaitTo.BeTrue()
                .EditShippingBlock.NewAddress.Click()
                .WaitSeconds(2)
                .EditShippingBlock.FirstName.Clear()
                .EditShippingBlock.FirstName.Set(TestDataService.SwedishFirstName)
                .EditShippingBlock.LastName.Clear()
                .EditShippingBlock.LastName.Set(TestDataService.SwedishLastName)
                .EditShippingBlock.StreetAddress.Clear()
                .EditShippingBlock.StreetAddress.Set(TestDataService.ShippingStreetAddress)
                .EditShippingBlock.ZipCode.Clear()
                .EditShippingBlock.ZipCode.Set(TestDataService.ShippingZipCode)
                .WaitSeconds(1)
                .Do(x =>
                {
                    if (x.EditShippingBlock.City.Value.Length == 0)
                    {
                        x.EditShippingBlock.City.Set(TestDataService.ShippingCity);
                    }
                })
                .EditShippingBlock.Submit.Focus()
                .EditShippingBlock.Submit.Click()
                .Do(x =>
                {
                    if (shipping.ContainsKey("carrier"))
                    {
                        var options = shipping["carrier"].ToList();

                        foreach (var option in options)
                        {
                            if (options.IndexOf(option) != 0)
                            {
                                x
                                .SelectShippingBlock.ChangeCarrier.Click()
                                .WaitSeconds(1);
                            }

                            x.SelectShippingBlock.Options.WaitTo.WithinSeconds(30).Not.BeEmpty();

                            x
                            .SelectShippingBlock.Options.FirstOrDefault(x => x.Text.Content.Value.Contains(option)).Click()
                            .WaitSeconds(1);
                        }
                    }
                    
                    if (shipping.ContainsKey("pickup"))
                    {
                        var pickups = shipping["pickup"].ToList();

                        foreach (var pickup in pickups)
                        {
                            x.SelectShippingBlock.ChangePickupPlace.Click();
                            x.WaitSeconds(1);

                            var index = int.Parse(pickup);

                            if(index == -1)
                            {
                                x.SelectShippingBlock.PickupList.Last().Click();
                            }
                            else
                            {
                                x.SelectShippingBlock.PickupList[index].Click();
                            }

                            x
                            .WaitSeconds(1)
                            .SelectShippingBlock.ConfirmChange.Focus()
                            .Press(Keys.Space);
                        }
                    }
                })
                .WaitSeconds(2)
                .Do(x =>
                {
                    switch (shipping["carrier"].Last())
                    {
                        default:
                        case ShippingOptions.Bring:
                            x
                            .SelectShippingBlock.DoorCode.Set(TestDataService.DoorCode)
                            .SelectShippingBlock.Instructions.Set(TestDataService.ShippingInstructions)
                            .WaitSeconds(1)
                            .SelectShippingBlock.Submit.Focus()
                            .SelectShippingBlock.Submit.Click();
                            break;

                        case ShippingOptions.Plab:
                            x
                            .SelectShippingBlock.Instructions.Set(TestDataService.ShippingInstructions)
                            .WaitSeconds(1)
                            .SelectShippingBlock.DeliveryBefore12.Click()
                            .WaitSeconds(1)
                            .SelectShippingBlock.CallBeforeDelivery.Click()
                            .WaitSeconds(1)
                            .SelectShippingBlock.Submit.Focus()
                            .SelectShippingBlock.Submit.Click();
                            break;

                        case ShippingOptions.Dhl:
                            x
                            .SelectShippingBlock.Instructions.Set(TestDataService.ShippingInstructions)
                            .SelectShippingBlock.DoorCode.Set(TestDataService.DoorCode)
                            .WaitSeconds(1)
                            .SelectShippingBlock.Submit.Focus()
                            .SelectShippingBlock.Submit.Click();
                            break;

                        case ShippingOptions.Budbee:
                            x
                            .SelectShippingBlock.Instructions.Set(TestDataService.ShippingInstructions)
                            .SelectShippingBlock.Indoor.Click()
                            .WaitSeconds(1)
                            .SelectShippingBlock.Submit.Focus()
                            .SelectShippingBlock.Submit.Click();
                            break;
                    }
                })
                ;
        }

        public static SveaPaymentFramePage Pay(this SveaPaymentFramePage page, Checkout.Option checkout = Checkout.Option.Identification, Entity.Option entity = Entity.Option.Private, PaymentMethods.Option paymentMethod = PaymentMethods.Option.Card, string amount = null, bool switchFrame = false)
        {
            switch (paymentMethod)
            {
                default:
                case PaymentMethods.Option.Card:
                case PaymentMethods.Option.CardEmbedded:
                    page.PaymentMethods.Card.IsVisible.WaitTo.WithinSeconds(30).BeTrue().PaymentMethods.Card.Click();
                    break;

                case PaymentMethods.Option.DirektBank:
                    page.PaymentMethods.DirektBank.IsVisible.WaitTo.WithinSeconds(30).BeTrue().PaymentMethods.DirektBank.Click();
                    break;

                case PaymentMethods.Option.Invoice:
                    page.PaymentMethods.Invoice.IsVisible.WaitTo.WithinSeconds(30).BeTrue().PaymentMethods.Invoice.Click();
                    break;

                case PaymentMethods.Option.Trustly:
                    page.PaymentMethods.Trustly.IsVisible.WaitTo.WithinSeconds(30).BeTrue().PaymentMethods.Trustly.Click();
                    break;

                case PaymentMethods.Option.PaymentPlan:
                    page.PaymentMethods.PaymentPlan.IsVisible.WaitTo.WithinSeconds(30).BeTrue().PaymentMethods.PaymentPlan.Click();
                    break;

                case PaymentMethods.Option.AccountCredit:
                    page.PaymentMethods.Account.IsVisible.WaitTo.WithinSeconds(30).BeTrue().PaymentMethods.Account.Click();
                    break;

                case PaymentMethods.Option.BlackFriday:
                    page.PaymentMethods.BlackFriday.IsVisible.WaitTo.WithinSeconds(30).BeTrue().PaymentMethods.BlackFriday.Click();
                    break;

                case PaymentMethods.Option.Swish:
                    page.PaymentMethods.Swish.IsVisible.WaitTo.WithinSeconds(30).BeTrue().PaymentMethods.Swish.Click();
                    break;

                case PaymentMethods.Option.Vipps:
                    page.PaymentMethods.Vipps.IsVisible.WaitTo.WithinSeconds(30).BeTrue().PaymentMethods.Vipps.Click();
                    break;

                case PaymentMethods.Option.Leasing:
                    page.PaymentMethods.Leasing.IsVisible.WaitTo.WithinSeconds(30).BeTrue().PaymentMethods.Leasing.Click();
                    break;

                case PaymentMethods.Option.MobilePay:
                    page.PaymentMethods.MobilePay.IsVisible.WaitTo.WithinSeconds(30).BeTrue().PaymentMethods.MobilePay.Click();
                    break;
            }

            if (entity == Entity.Option.Company && checkout == Checkout.Option.Identification)
            {
                page.WaitSeconds(1)
                    .PaymentMethods.Reference.Set(TestDataService.Reference);
            }

            switch (paymentMethod)
            {
                default:
                case PaymentMethods.Option.Card:
                    return page.PayWithCard(switchFrame);

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
