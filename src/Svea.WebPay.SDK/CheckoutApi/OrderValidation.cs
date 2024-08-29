using System;
using System.Collections.Generic;
using System.Text;

namespace Svea.WebPay.SDK.CheckoutApi
{
    public class OrderValidation
    {
        public OrderValidation() { }
        public OrderValidation(long? minAge)
        {
            MinAge = minAge;
        }
        public long? MinAge { get; }
    }
}
