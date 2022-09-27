using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinition("div[@data-testid='country-identification-view']", ComponentTypeName = "Entity Block")]
    public class EntityBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByClass("MuiTab-wrapper", Index = 0)]
        public Text<TOwner> IsPrivate { get; set; }

        [FindByClass("MuiTab-wrapper", Index = 1)]
        public Text<TOwner> IsCompany { get; set; }

    }
}
