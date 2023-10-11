using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinition("div[@data-testid='country-identification-view']", ComponentTypeName = "Entity Block")]
    public class EntityBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByClass("MuiTab-root", Index = 0)]
        public Text<TOwner> IsPrivate { get; set; }

        [FindByClass("MuiTab-root", Index = 1)]
        public Text<TOwner> IsCompany { get; set; }

    }
}
