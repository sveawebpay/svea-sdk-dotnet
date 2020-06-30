using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels.Base;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinitionAutomation("div-order", ComponentTypeName = "Order Block")]
    public class OrderBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByAutomation("text-orderid")]
        public Text<TOwner> OrderId { get; private set; }

        [FindByAutomation("text-orderstatus")]
        public Text<TOwner> OrderStatus { get; private set; }

        [FindByAutomation("text-paymenttype")]
        public Text<TOwner> PaymentType { get; private set; }

        public OrderTable<TOwner> Table { get; private set; }
    }
}
