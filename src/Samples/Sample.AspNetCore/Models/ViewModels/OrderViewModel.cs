using System.Collections.Generic;

namespace Sample.AspNetCore.Models.ViewModels
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    public class OrderViewModel
    {
        public Svea.WebPay.SDK.CheckoutApi.Data Order { get; set; }
        public List<Order> PaymentOrders { get; set; }
    }
}