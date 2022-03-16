using System.Linq;
using System.Threading.Tasks;

namespace Sample.AspNetCore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Sample.AspNetCore.Data;
    using Sample.AspNetCore.Models;

    using Svea.WebPay.SDK;
    using Svea.WebPay.SDK.CheckoutApi;

    using System;

    using Cart = Models.Cart;

    [ApiController]
    [Route("api/svea")]
    public class SveaController : ControllerBase
    {
        private readonly ILogger<SveaController> _logger;
        private readonly Cart _cartService;
        private readonly StoreDbContext _context;
        private readonly SveaWebPayClient _sveaClient;


        public SveaController(ILogger<SveaController> logger, Cart cartService, StoreDbContext context, SveaWebPayClient sveaClient)
        {
            _logger = logger;
            _cartService = cartService;
            _context = context;
            _sveaClient = sveaClient;
        }

        [HttpGet("validation/{orderId}")]
        public ActionResult Validation(long? orderId)
        {
            var response = new CheckoutValidationCallbackResponse(true);
            return Ok(response);
        }

        [HttpPost("push/{orderId}")]
        public async Task<IActionResult> Push(long? orderId)
        {
            try
            {
                if (orderId.HasValue)
                {
                    var order = await _sveaClient.Checkout.GetOrder(orderId.Value).ConfigureAwait(false);
                    if (order != null && order.Status == CheckoutOrderStatus.Final)
                    {
                        _cartService.SveaOrderId = order.OrderId.ToString();
                        _cartService.Update();

                        var products = _cartService.CartLines.Select(p => p.Product);

                        _context.Products.AttachRange(products);

                        var existing = _context.Orders.Find(order.OrderId.ToString());
                        if (existing != null)
                        {
                            return Ok();
                        }

                        _context.Orders.Add(new Order
                        {
                            SveaOrderId = _cartService.SveaOrderId,
                            Lines = _cartService.CartLines.ToList()
                        });
                        _context.SaveChanges(true);
                    }
                }

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Callback failed", e);
                return Ok();
            }
        }
    }
}
