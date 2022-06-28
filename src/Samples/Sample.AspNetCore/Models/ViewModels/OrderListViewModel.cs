using System.Collections.Generic;

namespace Sample.AspNetCore.Models.ViewModels
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    public class OrderListViewModel
    {
        public List<OrderViewModel> PaymentOrders { get; set; }
    }
}