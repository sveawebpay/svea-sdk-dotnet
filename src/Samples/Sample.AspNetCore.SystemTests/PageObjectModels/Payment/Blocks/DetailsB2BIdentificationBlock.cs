using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinition("form[@data-testid='b2b-identification-view']", ComponentTypeName = "Details B2B Identification Block")]
    public class DetailsB2BIdentificationBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByName("email")]
        public EmailInput<TOwner> Email { get; private set; }

        [FindByName("nationalId")]
        public TelInput<TOwner> OrganizationNumber { get; private set; }

        [FindByName("phoneNumber")]
        public TelInput<TOwner> Phone { get; private set; }
    }
}
