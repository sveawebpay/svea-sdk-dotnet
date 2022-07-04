using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sample.AspNetCore.Extensions;
using Sample.AspNetCore.Models;
using Svea.WebPay.SDK;
using Svea.WebPay.SDK.CheckoutApi;
using Svea.WebPay.SDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Cart = Sample.AspNetCore.Models.Cart;

namespace Sample.AspNetCore.Controllers
{


    public class CheckOutController : Controller
    {
        private readonly Cart _cartService;
        private readonly Market _marketService;
        private readonly SveaWebPayClient _sveaClient;
        private readonly Models.MerchantSettings _merchantSettings;


        public CheckOutController(
            IOptionsSnapshot<Models.MerchantSettings> merchantsAccessor,
            Cart cartService,
            Market marketService,
            SveaWebPayClient sveaClient)
        {
            _merchantSettings = merchantsAccessor.Value;
            _cartService = cartService;
            _marketService = marketService;
            _sveaClient = sveaClient;
        }


        public async Task<IActionResult> LoadPaymentMenu(bool requireBankId, bool isInternational, bool enableShipping = false)
        {
            var data = await CreatePaymentOrder(requireBankId, isInternational, enableShipping);

            var snippet = data.Gui.Snippet;

            var sveaCheckoutSource = new SveaCheckoutSource
            {
                Snippet = snippet
            };

            return View("Checkout", sveaCheckoutSource);
        }

        public async Task<Svea.WebPay.SDK.CheckoutApi.Data> CreatePaymentOrder(bool requireBanKId = false, bool isInternational = false, bool enableShipping = false)
        {
            var orderItems = _cartService.CartLines.ToOrderItems().ToList();
            try
            {
                var currencyRequest = new CurrencyCode(_marketService.CurrencyCode);
                var languageRequest = new Language(_marketService.LanguageId);
                var regionRequest = new RegionInfo(_marketService.MarketId);

                var region = isInternational ? new RegionInfo(_marketService.CountryId) : regionRequest;

                var pushUri = new Uri(_merchantSettings.PushUri.ToString().Replace("{marketId}", _marketService.MarketId));
                var checkoutValidationCallbackUri = new Uri(_merchantSettings.CheckoutValidationCallbackUri.ToString().Replace("{marketId}", _marketService.MarketId));

                //79d0c2d3-71f4-4205-a5bc-4aa9ab324c98 
                var shippingFallbacks = new List<FallbackOption> { new FallbackOption("875fb2cd-a570-4afb-8a66-177d3d613f81", "DHL Home Delivery", "dhl", Convert.ToInt64(_cartService.CalculateTotal())) };
                var shippingInformation = new ShippingInformation(true, 1000, null, shippingFallbacks);

                var shippingCallbackUri = new Uri(_merchantSettings.WebhookUri.ToString().Replace("{marketId}", _marketService.MarketId));

                var paymentOrderRequest = new CreateOrderModel(region, currencyRequest, languageRequest, DateTime.Now.Ticks.ToString(),
                    new Svea.WebPay.SDK.CheckoutApi.MerchantSettings(pushUri, _merchantSettings.TermsUri, _merchantSettings.CheckoutUri, _merchantSettings.ConfirmationUri, checkoutValidationCallbackUri, webhookUri: shippingCallbackUri),
                    new Svea.WebPay.SDK.CheckoutApi.Cart(orderItems), requireBanKId, null, null, null, null, enableShipping ? shippingInformation : null);
                
                var data = await _sveaClient.Checkout.CreateOrder(paymentOrderRequest).ConfigureAwait(false);

                return data;
            }
            catch (HttpResponseException ex)
            {
                Debug.Write(ex.Message);
                return null;
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