using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels;
using Sample.AspNetCore.SystemTests.Services;

namespace Sample.AspNetCore.SystemTests.Test.Helpers
{
    public static class IdentificationHelper
    {
        public static SveaPaymentFramePage ProceedWithSwedishPrivateIdentification(this SveaPaymentFramePage page)
        {
            return page
                .Entity.IsPrivate.Click()
                .B2CIdentification.Email.Set(TestDataService.Email)
                .B2CIdentification.ZipCode.Set(TestDataService.SwedishZipCode)
                
                .Submit.Click()
                .Do(x =>
                {
                    if (x.B2CIdentification.PersonalNumber.Exists(new SearchOptions { IsSafely = true }))
                    {
                        x
                        .B2CIdentification.PersonalNumber.Set(TestDataService.SwedishPersonalNumber)
                        .B2CIdentification.Phone.Set(TestDataService.SwedishPhoneNumber)
                        .Submit.Click();
                    }
                });
        }

        public static SveaPaymentFramePage ProceedWithNorwegianPrivateIdentification(this SveaPaymentFramePage page)
        {
            return page
                .Entity.IsPrivate.Click()
                .B2CIdentification.Email.Set(TestDataService.Email)
                .B2CIdentification.ZipCode.Set(TestDataService.NorwegianZipCode)
                .Submit.Click()
                .Do(x =>
                {
                    if (x.B2CIdentification.PersonalNumber.Exists(new SearchOptions { IsSafely = true }))
                    {
                        x
                        .B2CIdentification.PersonalNumber.Set(TestDataService.NorwegianPersonalNumber)
                        .B2CIdentification.Phone.Set(TestDataService.NorwegianPhoneNumber)
                        .Submit.Click();
                    }
                });
        }

        public static SveaPaymentFramePage ProceedWithFinnishPrivateIdentification(this SveaPaymentFramePage page)
        {
            return page
                .Entity.IsPrivate.Click()
                .B2CIdentification.Email.Set(TestDataService.Email)
                .B2CIdentification.ZipCode.Set(TestDataService.FinnishZipCode)
                .B2CIdentification.CreditAgreement.Click()
                .Submit.Click()
                .Do(x =>
                {
                    if (x.B2CIdentification.PersonalNumber.Exists(new SearchOptions { IsSafely = true }))
                    {
                        x
                        .B2CIdentification.Phone.Set(TestDataService.FinnishPhoneNumber)
                        .B2CIdentification.PersonalNumber.Set(TestDataService.FinnishPersonalNumber)
                        .Submit.Click();
                    }
                });
        }

        public static SveaPaymentFramePage ProceedWithCompanyIdentification(this SveaPaymentFramePage page)
        {
            return page
                .Entity.IsCompany.Click()
                .B2BIdentification.Email.Set(TestDataService.CompanyEmail)
                .B2BIdentification.OrganizationNumber.Set(TestDataService.OrganizationNumber)
                .Submit.Click()
                .Do(x =>
                {
                    if (x.B2BIdentification.Phone.Exists(new SearchOptions { IsSafely = true }))
                    {
                        x
                        .B2BIdentification.Phone.Set(TestDataService.SwedishPhoneNumber)
                        .Submit.Click();
                    }
                });
        }

        public static SveaPaymentFramePage ProceedWithPrivateAnonymous(this SveaPaymentFramePage page)
        {
            return page
                .Entity.IsPrivate.Click()
                .B2CIdentification.Email.Set("aaa@bbb.ccc")
                .B2CIdentification.ZipCode.Set(TestDataService.SwedishZipCode)
                .Submit.Click()
                .Do(x => 
                {
                    if (x.NotYou.IsVisible)
                    {
                        x.NotYou.Click();
                    }
                })
                .AnonymousToggle.Click()
                .B2CAnonymous.IsVisible.WaitTo.BeTrue()
                .B2CAnonymous.PhoneNumber.Set(TestDataService.SwedishPhoneNumber)
                .B2CAnonymous.FirstName.Set(TestDataService.SwedishFirstName)
                .B2CAnonymous.LastName.Set(TestDataService.SwedishLastName)
                .B2CAnonymous.Street.Set(TestDataService.SwedishStreet)
                .B2CAnonymous.City.Set(TestDataService.SwedishCity)
                .Submit.Click();
        }

        public static SveaPaymentFramePage ProceedWithCompanyAnonymous(this SveaPaymentFramePage page)
        {
            return page
                .Entity.IsCompany.Click()
                .AnonymousToggle.Click()
                .B2BAnonymous.IsVisible.WaitTo.BeTrue()
                .B2BAnonymous.Email.Set(TestDataService.Email)
                .B2BAnonymous.PhoneNumber.Set(TestDataService.SwedishPhoneNumber)
                .B2BAnonymous.OrganizationName.Set(TestDataService.CompanyName)
                .B2BAnonymous.Street.Set(TestDataService.SwedishStreet)
                .B2BAnonymous.ZipCode.Set(TestDataService.SwedishZipCode)
                .B2BAnonymous.City.Set(TestDataService.SwedishCity)
                .Submit.Click();
        }
    }
}
