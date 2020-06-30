using System.Linq;
using System.Threading.Tasks;

namespace Sample.AspNetCore.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Sample.AspNetCore.Data;
    using Sample.AspNetCore.Models;

    using Svea.WebPay.SDK;
    using Svea.WebPay.SDK.CheckoutApi;

    using Cart = Models.Cart;

    [ApiController]
    [Route("api/svea")]
    public class SveaController : ControllerBase
    {
        private readonly Cart _cartService;
        private readonly StoreDbContext _context;
        private readonly SveaWebPayClient _sveaClient;


        public SveaController(Cart cartService, StoreDbContext context, SveaWebPayClient sveaClient)
        {
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
                    var order = await _sveaClient.Checkout.GetOrder(orderId.Value);
                    if (order != null && order.Status == CheckoutOrderStatus.Final)
                    {
                        var paymentOrder = await _sveaClient.PaymentAdmin.GetOrder(orderId.Value);

                        if (paymentOrder != null)
                        {
                            _cartService.SveaOrderId = paymentOrder.Id.ToString();
                            _cartService.Update();

                            var products = _cartService.CartLines.Select(p => p.Product);

                            _context.Products.AttachRange(products);

                            var existing = _context.Orders.Find(paymentOrder.Id.ToString());
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
                }

                return Ok();
            }
            catch
            {
                return Ok();
            }
        }
    }
}
