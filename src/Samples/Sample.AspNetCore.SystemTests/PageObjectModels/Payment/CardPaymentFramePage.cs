using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    using _ = CardPaymentFramePage;

    public class CardPaymentFramePage : Page<_>
    {
        [FindByDescendantAttribute("value", values: "credit")]
        public Label<_> CreditCard { get; set; }

        [FindByDescendantAttribute("value", values: "debit")]
        public Label<_> DebitCard { get; set; }

        [FindById("CardNumber")]
        public TextInput<_> CardNumber { get; set; }

        [FindById("expdate")]
        public TextInput<_> Expiry { get; set; }

        [FindById("CVV")]
        public TextInput<_> Cvc { get; set; }

        [FindById("AmountDiv")]
        public TextInput<_> TotalAmount { get; set; }

        [FindById("submit-button")]
        public Button<_> Submit { get; set; }

    }
}