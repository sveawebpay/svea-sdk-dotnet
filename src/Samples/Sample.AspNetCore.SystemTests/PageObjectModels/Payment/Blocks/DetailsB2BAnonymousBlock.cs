using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinition("form[@data-testid='b2b-anonymous-identification-view']", ComponentTypeName = "Details B2B Anonymous Block")]
    public class DetailsB2BAnonymousBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {

        [FindByName("organizationName")]
        public TextInput<TOwner> OrganizationName { get; private set; }

        [FindByName("streetAddress")]
        public TextInput<TOwner> Street { get; private set; }

        [FindByName("postalCode")]
        public TelInput<TOwner> ZipCode { get; private set; }

        [FindByName("city")]
        public TextInput<TOwner> City { get; private set; }

        [FindByName("email")]
        public EmailInput<TOwner> Email { get; private set; }

        [FindByName("phoneNumber")]
        public TelInput<TOwner> PhoneNumber { get; private set; }

    }
}
