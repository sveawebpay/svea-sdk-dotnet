using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Base
{
    [WaitForDocumentReadyState]
    public abstract class BasePage<TOwner> : Page<TOwner>
        where TOwner : BasePage<TOwner>
    {
        [FindByClass("")] public Footer<TOwner> Footer { get; private set; }

        [FindByClass("navbar-nav")] public Header<TOwner> Header { get; private set; }

        [FindById("")] public TextInput<TOwner> SearchInput { get; private set; }

        [FindById("marketMenuButton")] public Button<TOwner> Market { get; private set; }

        [FindById("marketMenuDropdown", Visibility = Visibility.Visible)]
        public ControlList<Link<TOwner>, TOwner> Markets { get; private set; }

        [FindById("countryMenuButton")] public Button<TOwner> Country { get; private set; }

        [FindById("countryMenuDropdown", Visibility = Visibility.Visible)]
        public ControlList<Link<TOwner>, TOwner> Countries { get; private set; }

    }
}