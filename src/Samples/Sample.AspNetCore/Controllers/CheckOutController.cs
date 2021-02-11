using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sample.AspNetCore.Extensions;
using Sample.AspNetCore.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Svea.WebPay.SDK;
using Svea.WebPay.SDK.CheckoutApi;
using Cart = Sample.AspNetCore.Models.Cart;

namespace Sample.AspNetCore.Controllers
{
    using System.Globalization;

    public class CheckOutController : Controller
    {
        private readonly Cart _cartService;
        private readonly SveaWebPayClient _sveaClient;
        private readonly Models.MerchantSettings _merchantSettings;


    public CheckOutController(
        IOptionsSnapshot<Models.MerchantSettings> merchantsAccessor,
        Cart cartService,
        SveaWebPayClient sveaClient)
        {
            _merchantSettings = merchantsAccessor.Value;
            _cartService = cartService;
            _sveaClient = sveaClient;
        }


        public async Task<IActionResult> LoadPaymentMenu(bool requireBankId)
        {
            var data = await CreatePaymentOrder(null, requireBankId);

            var snippet = data.Gui.Snippet;

            var SveaCheckoutSource = new SveaCheckoutSource
            {
                Snippet = snippet
            };

            return View("Checkout", SveaCheckoutSource);
        }

        public async Task<Svea.WebPay.SDK.CheckoutApi.Data> CreatePaymentOrder(string consumerProfileRef = null, bool requireBanKId = false)
        {
            var orderItems = _cartService.CartLines.ToOrderItems().ToList();
            try
            {
                var noRegion = new RegionInfo("NO");
                var nok = new CurrencyCode("NOK");

                var usRegion = new RegionInfo("US");

                var seRegion = new RegionInfo("SE");
                var sek = new CurrencyCode("SEK");
                var paymentOrderRequest = new CreateOrderModel(seRegion, sek, new Language("sv-SE"), DateTime.Now.Ticks.ToString(),
                    new Svea.WebPay.SDK.CheckoutApi.MerchantSettings(_merchantSettings.PushUri, _merchantSettings.TermsUri, _merchantSettings.CheckoutUri, _merchantSettings.ConfirmationUri, _merchantSettings.CheckoutValidationCallbackUri),
                    new Svea.WebPay.SDK.CheckoutApi.Cart(orderItems), requireBanKId);

                var data = await _sveaClient.Checkout.CreateOrder(paymentOrderRequest);

                return data;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return null;
            }
        }

        public ViewResult Thankyou()
        {
            _cartService.Clear();
            return View();
        }
    }
}