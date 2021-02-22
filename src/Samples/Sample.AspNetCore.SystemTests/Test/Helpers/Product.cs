namespace Sample.AspNetCore.SystemTests.Test.Helpers
{
    public class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Currency { get; set; }
        public double UnitPrice { get; set; }
        public bool HasDiscount { get; set; }
        public double DiscountAmount { get; set; }
    }
}