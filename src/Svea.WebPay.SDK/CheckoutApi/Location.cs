namespace Svea.WebPay.SDK.CheckoutApi
{
    public class Location
    { 
        public Location(string id, string name, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public string Id { get; }
        public string Name { get; }
        public Address Address { get; }
    }
}
