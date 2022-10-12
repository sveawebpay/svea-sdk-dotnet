using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment.Blocks
{
    [ControlDefinition("my-shipping", ComponentTypeName = "Select Shipping Block")]
    public class SelectShippingBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        public ControlList<ShippingOptionItem<TOwner>, TOwner> Options { get; private set; }

        [FindByCss("*[data-testid='clear-selected-option-btn']")]
        public Button<TOwner> ChangeCarrier { get; private set; }

        [FindByCss("*[data-testid='change-location-link']")]
        public Button<TOwner> ChangePickupPlace { get; private set; }

        public ControlList<RadioButton<TOwner>, TOwner> PickupList { get; private set; }

        [FindByContent("Bekräfta")]
        public Button<TOwner> ConfirmChange { get; private set; }

        [FindById("fields.FCDELIVERYINSTRUCTIONS")]
        public Input<string, TOwner> Instructions { get; private set; }

        [FindByCss("*[data-testid='confirm-shipping-option']")]
        public Button<TOwner> Submit { get; private set; }


        #region Bring

        [FindById("fields.FCRECEIVERDOORCODE")]
        public Input<string, TOwner> DoorCode { get; private set; }

        #endregion

        #region Postnord

        [FindByCss("*[data-testid='FCDELIVERYTIME12']")]
        public CheckBox<TOwner> DeliveryBefore12 { get; private set; }

        [FindByCss("*[data-testid='FCNOTIFYPHONE']")]
        public CheckBox<TOwner> CallBeforeDelivery { get; private set; }

        #endregion

        #region Budbee

        [FindByCss("*[data-testid='FCINDOOR']")]
        public Clickable<TOwner> Indoor { get; private set; }

        #endregion
        
    }
}
