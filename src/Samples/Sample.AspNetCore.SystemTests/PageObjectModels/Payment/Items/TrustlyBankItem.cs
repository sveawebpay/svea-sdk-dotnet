using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
    [ControlDefinition("div[@data-testid^='list-item-deposit.bank.sweden']", ComponentTypeName = "Trustly Bank Item")]
    public class TrustlyBankItem<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
        
    }
}
