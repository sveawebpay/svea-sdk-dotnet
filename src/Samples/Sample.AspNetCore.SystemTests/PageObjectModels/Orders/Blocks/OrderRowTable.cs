using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels.Base;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinitionAutomation("table-orderrow", ComponentTypeName = "Order Row Table")]
    public class OrderRowTable<TOwner> : Table<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByAutomation("toggle-actions")]
        public Clickable<TOwner> Toggle { get; private set; }

        [FindByAutomation("a-cancelorderrow")]
        public Link<TOwner> CancelRow { get; private set; }

        [FindByAutomation("a-updateorderrow")]
        public Link<TOwner> UpdateRow { get; private set; }

        [FindFirst]
        public ControlList<Link<TOwner>, TOwner> AvailableActions { get; private set; }
    }
}
