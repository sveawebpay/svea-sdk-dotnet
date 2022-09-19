using Sample.AspNetFramework.Models;
using Svea.WebPay.SDK;
using Svea.WebPay.SDK.CheckoutApi;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sample.AspNetFramework.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var checkout = new HttpClient()
            {
                BaseAddress = new Uri("https://checkoutapistage.svea.com")
            };

            var payment = new HttpClient()
            {
                BaseAddress = new Uri("https://paymentadminapistage.svea.com")
            };


           
            var client = new SveaWebPayClient(checkout, payment, new Credentials("201663", "6LDeIkoe4MlMQ4EuSmC1TgQAO4i3SgyceIt2Jd89e7xgqJiwhifwTqU04czObuwXnjIhKLUOoCXVTJkeVWVKacsXYD7GNu8TLJ3gaaVGxBirC8gv2u2pJVXk1VthNna3"));

            var orderRows = new List<OrderRow> {
                new OrderRow("ref","name", 1, 10, 0, 0, null, null, 1)
            };

            var paymentOrderRequest = new CreateOrderModel(new RegionInfo("SE"), new CurrencyCode("SEK"), new Language("sv-SE"), DateTime.Now.Ticks.ToString(),
                             new Svea.WebPay.SDK.CheckoutApi.MerchantSettings(new Uri("https://svea-sample.ngrok.io/api"), new Uri("https://svea-sample.ngrok.io/api"), new Uri("https://svea-sample.ngrok.io/api"), new Uri("https://svea-sample.ngrok.io/api"), new Uri("https://svea-sample.ngrok.io/api")),
                             new Svea.WebPay.SDK.CheckoutApi.Cart(orderRows), true);

            var data = await client.Checkout.CreateOrder(paymentOrderRequest).ConfigureAwait(false);




            var snippet = data.Gui.Snippet;

            var model = new HomeViewModel { Snippet = snippet };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}