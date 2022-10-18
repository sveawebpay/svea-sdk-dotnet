using System.Collections.Generic;

namespace Svea.WebPay.SDK.CheckoutApi
{
    public class Addon
    {
        public Addon(string id, List<Field> fields)
        {
            Id = id;
            Fields = fields;
        }

        public string Id { get; }
        public List<Field> Fields { get; }
    }
}
