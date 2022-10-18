using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinition("div[@data-testid='expandable-card-header']", ComponentTypeName = "Shipping Option Item", Visibility = Visibility.Visible)]
    public class ShippingOptionItem<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        public Text<TOwner> Text { get; private set; }
    }
}
