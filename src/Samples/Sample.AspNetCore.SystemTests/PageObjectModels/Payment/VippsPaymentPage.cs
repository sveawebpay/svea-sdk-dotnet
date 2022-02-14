using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    using _ = VippsPaymentPage;

    public class VippsPaymentPage : Page<_>
    {
        [FindByName("pay")]
        public Button<_> Next { get; set; }

    }
}