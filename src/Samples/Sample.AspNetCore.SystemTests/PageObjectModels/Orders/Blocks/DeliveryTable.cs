using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels.Base;
using Sample.AspNetCore.SystemTests.PageObjectModels.Orders;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinitionAutomation("table-delivery", ComponentTypeName = "Order Row Table")]
    public class DeliveryTable<TOwner> : Table<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByAutomation("toggle-actions")]
        public Clickable<OrdersPage, TOwner> Toggle { get; private set; }
     
        [FindByAutomation("a-creditamount")]
        public Link<OrdersPage, TOwner> CreditAmount { get; private set; }

        [FindByAutomation("a-creditnewrow")]
        public Link<OrdersPage, TOwner> CreditNewRow { get; private set; }

        [FindByAutomation("a-creditorderrows")]
        public Link<OrdersPage, TOwner> CreditOrderRows { get; private set; }

        [FindByAutomation("a-creditorderrowswithfee")]
        public Link<OrdersPage, TOwner> CreditOrderRowsWithFee { get; private set; }

        [FindFirst]
        public ControlList<Link<TOwner>, TOwner> AvailableActions { get; private set; }

    }
}
