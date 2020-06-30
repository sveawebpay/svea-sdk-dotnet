using System.Collections.Generic;

namespace Svea.WebPay.SDK.CheckoutApi
{
    public class Cart
    {
        public Cart(IList<OrderRow> items)
        {
            Items = items;
        }

        public IList<OrderRow> Items { get; }
    }
}