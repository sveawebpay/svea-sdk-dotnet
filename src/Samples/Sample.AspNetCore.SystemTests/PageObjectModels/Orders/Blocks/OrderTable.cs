using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels.Base;
using Sample.AspNetCore.SystemTests.PageObjectModels.Orders;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinitionAutomation("table-order", ComponentTypeName = "Order Table")]
    public class OrderTable<TOwner> : Table<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByAutomation("toggle-actions")]
        public Clickable<TOwner> Toggle { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByAutomation("a-deliverorder")]
        public Link<OrdersPage, TOwner> DeliverOrder { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByAutomation("a-deliverorderpartially")]
        public Link<OrdersPage, TOwner> DeliverOrderPartially { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByAutomation("a-cancel")]
        public Link<OrdersPage, TOwner> CancelOrder { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByAutomation("a-cancelorderrows")]
        public Link<OrdersPage, TOwner> CancelOrderRows { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByAutomation("a-cancelamount")]
        public Link<OrdersPage, TOwner> CancelOrderAmount { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByAutomation("a-addorderrow")]
        public Button<OrdersPage, TOwner> AddOrderRow { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByAutomation("a-addorderrows")]
        public Button<OrdersPage, TOwner> AddOrderRows { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByAutomation("a-updateorderrows")]
        public Link<OrdersPage, TOwner> UpdateOrderRows { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByAutomation("a-replaceorderrows")]
        public Link<OrdersPage, TOwner> ReplaceOrderRows { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByAutomation("input-addorderrowpercentdiscount")]
        public NumberInput<TOwner> AddOrderRowPercentDiscount { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByAutomation("input-addorderrowamountdiscount")]
        public NumberInput<TOwner> AddOrderRowAmountDiscount { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByAutomation("input-addorderrowspercentdiscount")]
        public NumberInput<TOwner> AddOrderRowsPercentDiscount { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByAutomation("input-addorderrowsamountdiscount")]
        public NumberInput<TOwner> AddOrderRowsAmountDiscount { get; private set; }

        [FindFirst]
        public ControlList<Link<TOwner>, TOwner> AvailableActions { get; private set; }
    }
}
