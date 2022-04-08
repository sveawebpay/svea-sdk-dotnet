using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels.Base;
using Sample.AspNetCore.SystemTests.PageObjectModels.Payment;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Orders
{
    using _ = OrdersPage;

    public class OrdersPage : BasePage<_>
    {
        public ControlList<FullOrderBlock<_>, _> Orders { get; private set; }

        public FullOrderBlock<_> FirstOrder { get; private set; }

        [FindByAutomation("div-fullorder")]
        public Control<_> Details { get; private set; }
    }
}