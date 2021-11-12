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

        [FindByAutomation("a-deliverorder")]
        public Link<OrdersPage, TOwner> DeliverOrder { get; private set; }

        [FindByAutomation("a-deliverorderpartially")]
        public Link<OrdersPage, TOwner> DeliverOrderPartially { get; private set; }

        [FindByAutomation("a-cancel")]
        public Link<OrdersPage, TOwner> CancelOrder { get; private set; }

        [FindByAutomation("a-cancelorderrows")]
        public Link<OrdersPage, TOwner> CancelOrderRows { get; private set; }

        [FindByAutomation("a-cancelamount")]
        public Link<OrdersPage, TOwner> CancelOrderAmount { get; private set; }

        [FindByAutomation("a-addorderrow")]
        public Button<OrdersPage, TOwner> AddOrderRow { get; private set; }

        [FindByAutomation("a-addorderrows")]
        public Button<OrdersPage, TOwner> AddOrderRows { get; private set; }

        [FindByAutomation("a-updateorderrows")]
        public Link<OrdersPage, TOwner> UpdateOrderRows { get; private set; }

        [FindByAutomation("a-replaceorderrows")]
        public Link<OrdersPage, TOwner> ReplaceOrderRows { get; private set; }

        [FindByAutomation("input-addorderrowpercentdiscount")]
        public NumberInput<TOwner> AddOrderRowPercentDiscount { get; private set; }

        [FindByAutomation("input-addorderrowamountdiscount")]
        public NumberInput<TOwner> AddOrderRowAmountDiscount { get; private set; }

        [FindByAutomation("input-addorderrowspercentdiscount")]
        public NumberInput<TOwner> AddOrderRowsPercentDiscount { get; private set; }

        [FindByAutomation("input-addorderrowsamountdiscount")]
        public NumberInput<TOwner> AddOrderRowsAmountDiscount { get; private set; }

        [FindFirst]
        public ControlList<Link<TOwner>, TOwner> AvailableActions { get; private set; }
    }
}
