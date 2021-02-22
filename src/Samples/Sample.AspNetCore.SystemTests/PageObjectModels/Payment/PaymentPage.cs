using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels.Base;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    using _ = PaymentPage;

    public class PaymentPage : BasePage<_>
    {
        [FindById("svea-checkout-iframe")] 
        public Frame<SveaPaymentFramePage, _> SveaFrame { get; set; }
    }
}