namespace Svea.WebPay.SDK.CheckoutApi
{
    public interface IShippingOption
    {
        string Id { get; }
        string Carrier { get; }
        string Name { get; }
        long Price { get; }
    }
}