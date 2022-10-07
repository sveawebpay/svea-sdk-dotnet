
namespace Svea.WebPay.SDK.CheckoutApi
{
    using System.Text.Json.Serialization;

    public class ShippingCallbackDescription
    {
        public ShippingCallbackDescription(string tmsReference, ShippingOptionResponse selectedShippingOption)
        {
            TmsReference = tmsReference;
            SelectedShippingOption = selectedShippingOption;
        }

        [JsonPropertyName("tmsReference")]
        public string TmsReference { get; }

        [JsonPropertyName("selectedShippingOption")]
        public ShippingOptionResponse SelectedShippingOption { get; }
    }
}
