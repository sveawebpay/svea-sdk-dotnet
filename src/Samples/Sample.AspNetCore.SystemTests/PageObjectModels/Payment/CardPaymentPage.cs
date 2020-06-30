using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    using _ = CardPaymentPage;

    public class CardPaymentPage : Page<_>
    {
        [FindById("CardNumber")] 
        public TextInput<_> CardNumber { get; set; }

        [FindById("Expiry")]
        public TextInput<_> Expiry { get; set; }

        [FindById("CVV")]
        public TextInput<_> Cvc { get; set; }

        [FindById("AmountDiv")]
        public TextInput<_> TotalAmount { get; set; }
        
        [FindById("submit-button")]
        public Button<_> Submit { get; set; }

    }
}