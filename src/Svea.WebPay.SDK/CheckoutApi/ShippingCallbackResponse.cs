namespace Svea.WebPay.SDK.CheckoutApi
{
    using System.Text.Json.Serialization;

    public class ShippingCallbackResponse
    {
        public ShippingCallbackResponse(string type, string description, long orderId)
        {
            Type = type;
            Description = description;
            OrderId = orderId;
        }

        public string Type { get; }
        public string Description { get; }
        public long OrderId { get; }
    }
}
