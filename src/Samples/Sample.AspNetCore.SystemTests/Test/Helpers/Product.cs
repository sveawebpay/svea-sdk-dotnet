namespace Sample.AspNetCore.SystemTests.Test.Helpers
{
    public class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Currency { get; set; }
        public double UnitPrice { get; set; }
        public bool HasAmountDiscount { get; set; }
        public bool HasPercentDiscount { get; set; }
        public double AmountDiscount { get; set; }
        public double PercentDiscount { get; set; }
    }
}