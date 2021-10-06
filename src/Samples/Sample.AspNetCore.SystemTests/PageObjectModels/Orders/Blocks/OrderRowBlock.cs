using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels.Base;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinitionAutomation("div-orderrow", ComponentTypeName = "Order Row Block")]
    public class OrderRowBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByAutomation("text-orderrowid")]
        public Text<TOwner> OrderRowId { get; private set; }

        [FindByAutomation("text-orderrowname")]
        public Text<TOwner> Name { get; private set; }

        [FindByAutomation("text-orderquantity")]
        public Text<TOwner> Quantity { get; private set; }

        [FindByAutomation("text-orderrowcancelled")]
        public Text<TOwner> IsCancelled { get; private set; }

        public OrderRowTable<TOwner> Table { get; private set; }
    }
}
