using System.Collections.Generic;
using System.Linq;

namespace Svea.WebPay.SDK.CheckoutApi
{
    public class CartBase
    {
        public CartBase(IList<OrderRowBase> items)
        {
            Items = items;
        }

        public IList<OrderRowBase> Items { get; }

  
    }
}
