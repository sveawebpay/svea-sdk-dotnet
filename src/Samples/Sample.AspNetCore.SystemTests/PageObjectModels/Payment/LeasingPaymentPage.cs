using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    public class LeasingPaymentBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByCss("div[data-testid='leasing-manual-check-dialog']")]
        public Control<TOwner> ManualConfirmation { get; set; }

        [FindByCss("button[data-testid='confirm-button']")]
        public Button<TOwner> Confirm { get; set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByCss("label[data-testid='YLeasing_TLeasing_O28_S']")]
        public Clickable<TOwner> SixtyMonths { get; set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByCss("button[data-testid='continue-button']")]
        public Button<TOwner> Continue { get; set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByName("email")]
        public EmailInput<TOwner> Email { get; set; }

    }
}