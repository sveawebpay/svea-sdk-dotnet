using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels.Base;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinitionAutomation("div-fullorder", ComponentTypeName = "Full Order Block")]
    public class FullOrderBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        public OrderBlock<TOwner> Order { get; private set; }

        public ControlList<OrderRowBlock<TOwner>, TOwner> OrderRows { get; private set; }

        public ControlList<DeliveryBlock<TOwner>, TOwner> Deliveries { get; private set; }
    }
}
