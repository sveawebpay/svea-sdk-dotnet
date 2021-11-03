using System.Collections.Generic;

namespace Svea.WebPay.SDK.CheckoutApi
{
    public class CartResponse
    {
        public CartResponse(IList<OrderRowResponse> items)
        {
            Items = items;
        }

        public IList<OrderRowResponse> Items { get; }
    }
}