using System;
using System.Collections.Generic;
using System.Text;

namespace Svea.WebPay.SDK.CheckoutApi.Recurring
{
    public class PatchRecurringTokenModel
    {
        /// <summary>
        /// Status for recurring token,
        /// Can only be Cancelled for now...
        /// </summary>
        public TokenStatus Status { get; set; }
    }
}
