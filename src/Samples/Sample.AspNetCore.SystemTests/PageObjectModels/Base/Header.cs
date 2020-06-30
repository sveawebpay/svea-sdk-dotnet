using Atata;

using Sample.AspNetCore.SystemTests.PageObjectModels.Orders;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Base
{
    public class Header<TOwner> : Control<TOwner>
        where TOwner : BasePage<TOwner>
    {
        [Wait(0.5, TriggerEvents.AfterClick)]
        [FindByAutomation("button-clearorders")]
        public Button<HomePage, TOwner> ClearOrders { get; private set; }

        [Wait(3, TriggerEvents.BeforeClick)]
        [FindByContent("Orders")] public Link<OrdersPage, TOwner> Orders { get; private set; }

        [FindByContent("Products")] public Link<ProductsPage, TOwner> Products { get; private set; }
    }
}