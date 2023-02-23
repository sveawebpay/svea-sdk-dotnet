using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Payment
{
	[ControlDefinition("form[@data-testid='billing-reference-edit-view']", ComponentTypeName = "Reference Block")]
	public class InvoiceReferenceBlock<TOwner> : Control<TOwner> where TOwner : PageObject<TOwner>
    {
		[FindByClass("MuiSelect-select")]
		public Clickable<TOwner> Dropdown { get; private set; }

		[FindByClass("MuiListItem-root", Index = 1)]
		public Clickable<TOwner> InvoiceReferenceType { get; private set; }

		[FindByCss("*[data-testid='billing-reference']")]
		public TextInput<TOwner> Reference { get; private set; }

		[FindByCss("*[data-testid='submit-button']")]
		public Button<TOwner> Save { get; private set; }
	}
	
}
