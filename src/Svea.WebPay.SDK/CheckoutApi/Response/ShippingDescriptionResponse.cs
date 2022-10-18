namespace Svea.WebPay.SDK.CheckoutApi.Response
{
    internal class ShippingDescriptionResponse
    {
        internal ShippingDescriptionResponse(string tmsReference, ShippingOptionResponse selectedShippingOption)
        {
            TmsReference = tmsReference;
            SelectedShippingOption = selectedShippingOption;
        }

        internal string TmsReference { get; }

        internal ShippingOptionResponse SelectedShippingOption { get; }
    }
}
