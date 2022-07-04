using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment.Blocks
{
    [ControlDefinition("*[@data-testid='edit-address-view']", ComponentTypeName = "Edit Address Block")]
    public class EditShippingBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByCss("*[data-testid='new-address']")]
        public Clickable<TOwner> NewAddress { get; private set; }

        [FindByCss("*[data-testid='address-form-fields-b2c']")]
        public Control<TOwner> AddressForm { get; private set; }

        [FindByName("firstName")]
        public TextInput<TOwner> FirstName { get; private set; }

        [FindByName("lastName")]
        public TextInput<TOwner> LastName { get; private set; }

        [FindByName("streetAddress")]
        public TextInput<TOwner> StreetAddress { get; private set; }

        [FindByName("postalCode")]
        public TelInput<TOwner> ZipCode { get; private set; }

        [FindByName("city")]
        public TextInput<TOwner> City { get; private set; }

        [FindByCss("*[data-testid='submit-button']")]
        public Button<TOwner> Submit { get; private set; }
    }
}
