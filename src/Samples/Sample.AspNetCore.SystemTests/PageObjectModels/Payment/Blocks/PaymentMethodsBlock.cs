using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinition("form[@data-testid='confirm-form']", ComponentTypeName = "Payment Methods Block")]
    public class PaymentMethodsBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByCss("*[data-testid='card-view']")]
        public Clickable<TOwner> Card { get; private set; }

        public DirektBankItem<TOwner> DirektBank { get; private set; }

        [FindByCss("*[data-testid='trustly-view']")]
        public Clickable<TOwner> Trustly { get; private set; }

        [FindByCss("*[data-testid='promoted-view']")]
        public Clickable<TOwner> BlackFriday { get; private set; }

        [FindByCss("*[data-testid='invoice-view']")]
        public Clickable<TOwner> Invoice { get; private set; }

        [FindByCss("*[data-testid='swish-view']")]
        public Clickable<TOwner> Swish { get; private set; }

        [FindByCss("*[data-testid='vipps-view']")]
        public Clickable<TOwner> Vipps { get; private set; }

        [FindByCss("*[data-testid='leasing-view']")]
        public Clickable<TOwner> Leasing { get; private set; }

        [FindByCss("*[data-testid='mobilepay-view']")]
        public Clickable<TOwner> MobilePay { get; private set; }

        public PaymentPlanItem<TOwner> PaymentPlan { get; private set; }

        [FindByCss("*[data-testid='account-credit-view']")]
        public Clickable<TOwner> Account { get; private set; }

        [FindByCss("*[data-testid='totalAmount']")]
        public H1<TOwner> TotalAmount { get; private set; }

        [FindByName("orderReference")]
        public TextInput<TOwner> Reference { get; private set; }

    }
}
