using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment.Blocks
{
    [ControlDefinition("my-shipping", ComponentTypeName = "Select Shipping Block")]
    public class SelectShippingBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByCss("*[data-testid='expandable-card-header']")]
        public ItemsControl<Clickable<TOwner>, TOwner> Option { get; private set; }

        [FindById("fields.FCRECEIVERDOORCODE")]
        public Input<string, TOwner> DoorCode { get; private set; }

        [FindById("fields.FCDELIVERYINSTRUCTIONS")]
        public Input<string, TOwner> Instructions { get; private set; }

        [FindByCss("*[data-testid='confirm-shipping-option']")]
        public Button<TOwner> Submit { get; private set; }
    }
}
