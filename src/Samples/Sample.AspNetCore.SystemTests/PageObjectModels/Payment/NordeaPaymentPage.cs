using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    using _ = NordeaPaymentPage;

    public class NordeaPaymentPage : Page<_>
    {
        [FindByContent("Lyckad betalning")] 
        public Button<_> Success { get; set; }

        [FindByContent("Avbruten betalning")]
        public Button<_> Cancel { get; set; }

        [FindByContent("Avvisad betalning")]
        public Button<_> Reject { get; set; }

    }
}