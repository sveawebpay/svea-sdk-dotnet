using System.Collections.Generic;

namespace Svea.WebPay.SDK.CheckoutApi
{
    public interface IShippingOption
    {
        string Id { get; }
        string Carrier { get; }
        string Name { get; }
        List<Addon> Addons { get; }
        List<Field> Fields { get; }
    }
}