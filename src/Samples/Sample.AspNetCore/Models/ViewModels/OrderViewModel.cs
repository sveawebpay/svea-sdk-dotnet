namespace Sample.AspNetCore.Models.ViewModels
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    public class OrderViewModel
    {
        public OrderViewModel(int orderId)
        {
            OrderId = orderId;
        }
        public int OrderId { get; set; }
        public bool IsLoaded { get; set; }
        public Order Order { get; set; }
    }
}