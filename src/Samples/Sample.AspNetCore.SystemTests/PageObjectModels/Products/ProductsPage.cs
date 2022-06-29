using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels.Base;
using Sample.AspNetCore.SystemTests.PageObjectModels.Payment;

namespace Sample.AspNetCore.SystemTests.PageObjectModels
{
    using _ = ProductsPage;

    [WaitSeconds(1, TriggerEvents.Init)]
    public class ProductsPage : BasePage<_>
    {
        [FindByXPath("table[2]")] public Table<ProductBasketItem, _> CartProducts { get; set; }

        [FindByClass("alert alert-success")] public Text<_> Message { get; set; }

        [FindByXPath("table[1]")] public Table<ProductItem, _> Products { get; set; }

        [FindByAutomation("button-checkout")]
        public Link<PaymentPage, _> AnonymousCheckout { get; set; }

        [FindByAutomation("button-checkout-require")]
        public Link<PaymentPage, _> CheckoutAndRequireBankId { get; set; }

        [FindByAutomation("button-checkout-international")]
        public Link<PaymentPage, _> InternationalCheckout { get; set; }

        [FindByXPath("table[2]//tfoot[1]//td[2]")]
        public Text<_> TotalAmount { get; set; }

        public class ProductBasketItem : TableRow<_>
        {
            [FindByXPath("td[2]")] public Text<_> Name { get; set; }

            [FindByXPath("td[3]")] public Text<_> Price { get; set; }

            [FindByName("Quantity")] public NumberInput<_> Quantity { get; set; }

            [WaitSeconds(0.5, TriggerEvents.AfterClick)]
            [FindByXPath("button[1]")]
            public Button<_> Update { get; set; }
        }

        public class ProductItem : TableRow<_>
        {
            [FindByAutomation("button-addtocart")]
            public Link<_> AddToCart { get; set; }

            [FindByXPath("td[1]")] public Text<_> Name { get; set; }

            [FindByXPath("a[1]")] public Link<_> Open { get; set; }

            [FindByXPath("td[5]")] public Text<_> Price { get; set; }

            [FindByXPath("td[6]")] public Text<_> AmountDiscount { get; set; }

            [FindByXPath("td[7]")] public Text<_> PercentDiscount { get; set; }
        }
    }
}