using Svea.WebPay.SDK.CheckoutApi;
using Svea.WebPay.SDK.PaymentAdminApi;

namespace Svea.WebPay.SDK
{
    public interface ISveaClient
    {
        Checkout Checkout { get; }
        PaymentAdmin PaymentAdmin { get; }
    }
}