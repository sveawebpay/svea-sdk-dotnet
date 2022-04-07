using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinition("form[@data-testid='identify-international-form']", ComponentTypeName = "International Identification Block")]
    public class InternationalIdentificationBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByName("firstName")]
        public TextInput<TOwner> FirstName { get; private set; }

        [FindByName("lastName")]
        public TextInput<TOwner> LastName { get; private set; }

        [FindByName("streetAddress1")]
        public TextInput<TOwner> Street { get; private set; }

        [FindByName("postalCode")]
        public TextInput<TOwner> ZipCode { get; private set; }

        [FindByName("city")]
        public TextInput<TOwner> City { get; private set; }

        [FindByName("email")]
        public EmailInput<TOwner> Email { get; private set; }

        [FindByName("phoneNumber")]
        public TelInput<TOwner> PhoneNumber { get; private set; }

        [FindByName("country")]
        public TextInput<TOwner> Country { get; private set; }

    }
}
