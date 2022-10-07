using System.Text.Json.Serialization;

namespace Svea.WebPay.SDK.CheckoutApi
{
    public class ShippingOptionResponse
    {
        internal ShippingOptionResponse(string id, string carrier)
        {
            Id = id;
            Carrier = carrier;
        }
        
        public string Id { get; }
        
        public string Carrier { get; }
    }
}
