
namespace Svea.WebPay.SDK.CheckoutApi
{
    using System.Text.Json.Serialization;

    public class ShippingCallbackDescription
    {
        internal ShippingCallbackDescription(string tmsReference, ShippingOptionResponse selectedShippingOption)
        {
            TmsReference = tmsReference;
            SelectedShippingOption = selectedShippingOption;
        }
        
        public string TmsReference { get; }
        
        public ShippingOptionResponse SelectedShippingOption { get; }
    }
}
