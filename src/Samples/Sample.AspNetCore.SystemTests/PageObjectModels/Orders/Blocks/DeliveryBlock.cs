using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels.Base;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinitionAutomation("div-delivery", ComponentTypeName = "Delivery Block")]
    public class DeliveryBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        [FindByAutomation("text-deliveryid")]
        public Text<TOwner> DeliveryId { get; private set; }

        [FindByAutomation("text-deliverystatus")]
        public Text<TOwner> Status { get; private set; }

        [FindByAutomation("text-deliveryamount")]
        public Text<TOwner> DeliveryAmount { get; private set; }

        [FindByAutomation("text-deliverycreditedamount")]
        public Text<TOwner> CreditedAmount { get; private set; }

        public DeliveryTable<TOwner> Table { get; private set; }
    }
}
