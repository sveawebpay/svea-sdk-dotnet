using Microsoft.AspNetCore.Mvc;

using Sample.AspNetCore.Models;

namespace Sample.AspNetCore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly Cart cart;
        private readonly Market marketService;

        public CartSummaryViewComponent(Cart cartService, Market marketService)
        {
            this.cart = cartService;
            this.marketService = marketService;
        }


        public IViewComponentResult Invoke()
        {
            var m = marketService.MarketId;
            var c = marketService.CountryId;

            this.cart.IsInternational = m == "FI";
            return View(this.cart);
        }
    }
}