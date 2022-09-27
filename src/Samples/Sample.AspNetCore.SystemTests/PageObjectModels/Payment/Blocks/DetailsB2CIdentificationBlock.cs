using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinition("form[@data-testid='b2c-identification-view']", ComponentTypeName = "Details B2C Identification Block")]
    public class DetailsB2CIdentificationBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByName("email")]
        public EmailInput<TOwner> Email { get; private set; }

        [FindByName("postalCode")]
        public TelInput<TOwner> ZipCode { get; private set; }

        [FindByName("nationalId")]
        public TelInput<TOwner> PersonalNumber { get; private set; }

        [FindByName("phoneNumber")]
        public TelInput<TOwner> Phone { get; private set; }

        [FindByName("creditAgreement")]
        public Clickable<TOwner> CreditAgreement { get; private set; }
    }
}
