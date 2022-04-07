using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinition("form[@data-testid='b2c-anonymous-identification-view']", ComponentTypeName = "Details B2C Anonymous Block")]
    public class DetailsB2CAnonymousBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByName("firstName")]
        public TextInput<TOwner> FirstName { get; private set; }

        [FindByName("lastName")]
        public TextInput<TOwner> LastName { get; private set; }

        [FindByName("streetAddress")]
        public TextInput<TOwner> Street { get; private set; }

        [FindByCss("button[data-testid='co-address-button']")]
        public Button<TOwner> CareOfToggle { get; private set; }

        [FindByName("reference")]
        public TextInput<TOwner> CareOf { get; private set; }

        [FindByName("postalCode")]
        public TelInput<TOwner> ZipCode { get; private set; }

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
