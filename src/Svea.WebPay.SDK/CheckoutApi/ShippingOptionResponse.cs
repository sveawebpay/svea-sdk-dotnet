using System.Text.Json.Serialization;

namespace Svea.WebPay.SDK.CheckoutApi
{
    public class ShippingOptionResponse
    {
        public ShippingOptionResponse(string id, string carrier)
        {
            Id = id;
            Carrier = carrier;
        }

        [JsonPropertyName("id")]
        public string Id { get; }

        [JsonPropertyName("carrier")]
        public string Carrier { get; }
    }
}
