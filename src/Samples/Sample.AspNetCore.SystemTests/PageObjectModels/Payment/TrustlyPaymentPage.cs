using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    using _ = TrustlyPaymentPage;

    public class TrustlyPaymentPage : Page<_>
    {
        [FindByXPath("ul[@data-testid='data-list']")]
        public ControlList<TrustlyBankItem<_>, _> Banks { get; set; }

        [FindByContent("SEB")]
        public Clickable<_> Bank { get; set; }

        [FindByName("loginid")]
        public TelInput<_> PersonalNumber { get; private set; }

        [FindByContent("Säkerhetskod")]
        public Clickable<_> SecurityCodeOption { get; private set; }

        [FindByContent("Mobilt BankID")]
        public Clickable<_> BankIdOption { get; private set; }

        [FindByName("challenge_response")]
        public PasswordInput<_> Code { get; private set; }

        [FindFirst(Visibility = Visibility.Visible)]
        public H3<_> MessageCode { get; private set; }

        [FindByClass("options")]
        public Control<_> AccountOptions { get; private set; }

        [FindByContent("Checking account")]
        public Clickable<_> CheckingAccount { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByContent("Fortsätt")]
        public Button<_> Next { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByContent("Bekräfta betalning")]
        public Button<_> Confirm { get; private set; }
    }
}