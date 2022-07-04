using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment.Blocks
{
    [ControlDefinition("*[@data-testid='add-shipping-address-link']", ComponentTypeName = "Add Shipping Block")]
    public class AddShippingBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByCss("*[data-testid='expandable-card-header']")]
        public Clickable<TOwner> Expand { get; private set; }
    }
}
