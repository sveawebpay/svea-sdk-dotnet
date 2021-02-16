using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinition("form[@data-testid='identify-international-form']", ComponentTypeName = "International Identification Block")]
    public class InternationalIdentificationBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByName("country")]
        public TextInput<TOwner> Country { get; private set; }

    }
}
