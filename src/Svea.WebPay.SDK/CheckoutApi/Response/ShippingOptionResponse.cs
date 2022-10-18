namespace Svea.WebPay.SDK.CheckoutApi.Response
{
    internal class ShippingOptionResponse
    {
        internal ShippingOptionResponse(string id, string carrier)
        {
            Id = id;
            Carrier = carrier;
        }

        internal string Id { get; }
        internal string Carrier { get; }
    }
}
