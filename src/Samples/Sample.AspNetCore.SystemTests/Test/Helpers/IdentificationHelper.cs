using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels;
using Sample.AspNetCore.SystemTests.Services;

namespace Sample.AspNetCore.SystemTests.Test.Helpers
{
    public static class IdentificationHelper
    {
        public static SveaPaymentFramePage ProceedWithPrivateIdentification(this SveaPaymentFramePage page)
        {
            return page
                .Entity.IsPrivate.Click()
                .B2CIdentification.Email.Set(TestDataService.Email)
                .B2CIdentification.ZipCode.Set(TestDataService.ZipCode)
                .Submit.Click()
                .Do(x =>
                {
                    if (x.B2CIdentification.PersonalNumber.Exists(new SearchOptions { IsSafely = true }))
                    {
                        x
                        .B2CIdentification.PersonalNumber.Set(TestDataService.PersonalNumber)
                        .B2CIdentification.Phone.Set(TestDataService.PhoneNumber)
                        .Submit.Click();
                    }
                });

        }

        public static SveaPaymentFramePage ProceedWithCompanyIdentification(this SveaPaymentFramePage page)
        {
            return page
                .Entity.IsCompany.Click()
                .B2BIdentification.Email.Set(TestDataService.Email)
                .B2BIdentification.OrganizationNumber.Set(TestDataService.OrganizationNumber)
                .Submit.Click()
                .Do(x =>
                {
                    if (x.B2BIdentification.Phone.Exists(new SearchOptions { IsSafely = true }))
                    {
                        x
                        .B2BIdentification.Phone.Set(TestDataService.PhoneNumber)
                        .Submit.Click();
                    }
                });
        }

        public static SveaPaymentFramePage ProceedWithPrivateAnonymous(this SveaPaymentFramePage page)
        {
            return page
                .Entity.IsPrivate.Click()
                .Entity.ToggleIdentification.Click()
                .B2CAnonymous.IsVisible.WaitTo.BeTrue()
                .B2CAnonymous.FirstName.Set(TestDataService.FirstName)
                .B2CAnonymous.LastName.Set(TestDataService.LastName)
                .B2CAnonymous.Street.Set(TestDataService.Street)
                .B2CAnonymous.CareOfToggle.Click()
                .B2CAnonymous.CareOf.Set(TestDataService.CareOf)
                .B2CAnonymous.ZipCode.Set(TestDataService.ZipCode)
                .B2CAnonymous.City.Set(TestDataService.City)
                .B2CAnonymous.Email.Set(TestDataService.Email)
                .B2CAnonymous.PhoneNumber.Set(TestDataService.PhoneNumber)
                .Submit.Click();
        }

        public static SveaPaymentFramePage ProceedWithCompanyAnonymous(this SveaPaymentFramePage page)
        {
            return page
                .Entity.IsCompany.Click()
                .Entity.ToggleIdentification.Click()
                .B2BAnonymous.IsVisible.WaitTo.BeTrue()
                .B2BAnonymous.Email.Set(TestDataService.Email)
                .B2BAnonymous.PhoneNumber.Set(TestDataService.PhoneNumber)
                .B2BAnonymous.OrganizationName.Set(TestDataService.CompanyName)
                .B2BAnonymous.Street.Set(TestDataService.Street)
                .B2BAnonymous.ZipCode.Set(TestDataService.ZipCode)
                .B2BAnonymous.City.Set(TestDataService.City)
                .B2BAnonymous.Email.Set(TestDataService.Email)
                .B2BAnonymous.PhoneNumber.Set(TestDataService.PhoneNumber)
                .Submit.Click();
        }
    }
}
