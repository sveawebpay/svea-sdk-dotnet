namespace Svea.WebPay.SDK.CheckoutApi
{
    public class ShippingOption : IShippingOption
    {
        public ShippingOption(string id, string orderId, string carrier, string name, long price)
        {
            Id = id;
            OrderId = orderId;
            Carrier = carrier;
            Name = name;
            Price = price;
        }

        public string Id { get; }
        public string OrderId { get; }
        public string Carrier { get; }
        public string Name { get; }
        public long Price { get; }
    }

    public interface IShippingOption
    {
        string Id { get; }
        string OrderId { get; }
        string Carrier { get; }
        string Name { get; }
        long Price { get; }
    }
}
