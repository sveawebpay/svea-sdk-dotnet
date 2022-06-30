namespace Svea.WebPay.SDK.Tests.UnitTests
{
    using Svea.WebPay.SDK.CheckoutApi;

    using System.Collections.Generic;
    using System.Linq;

    using Xunit;

    public class CartTests
    {
        [Theory]
        [MemberData(nameof(GetCalculateShippingOrderRowsTestData), parameters: 3)]
        public void ShouldCalculateShippingOrderRows_AsExpected(IList<OrderRow> orderRows, ShippingOption shippingOption, decimal expected)
        {
            var cart = new Cart(orderRows);
                        cart.CalculateShippingOrderRows(shippingOption);
            long taxSum = 0;
            var shippingRows = cart.Items.Where(x => x.RowType == RowType.ShippingFee.ToString()).ToList();
            foreach (var shippingOrderRow in shippingRows)
            {
                var tax = shippingOrderRow.UnitPrice.InLowestMonetaryUnit * (shippingOrderRow.VatPercent / 100)/100;
                taxSum += tax.InLowestMonetaryUnit;
            }

            var actual = shippingRows.Sum(x => (x.UnitPrice.InLowestMonetaryUnit * (x.VatPercent / 100))/100);
            Assert.Equal(expected, taxSum/100M);
        }

        public static IEnumerable<object[]> GetCalculateShippingOrderRowsTestData(int numTests)
        {
            var orderRow1 = new OrderRow("articleNumber", "name", new MinorUnit(1), new MinorUnit(1000), new MinorUnit(200), new MinorUnit(25), "pcs", null, 1);
            var orderRow2 = new OrderRow("articleNumber2", "name2", new MinorUnit(1), new MinorUnit(200), new MinorUnit(0), new MinorUnit(6), "pcs", null, 3);
            var orderRow3 = new OrderRow("articleNumber2", "name2", new MinorUnit(2), new MinorUnit(350), new MinorUnit(0), new MinorUnit(12), "pcs", null, 2);
            var orderRow4 = new OrderRow("articleNumber", "name", new MinorUnit(5), new MinorUnit(350), new MinorUnit(0), new MinorUnit(25), "pcs", null, 4);

            var shippingOption1 = new ShippingOption("875fb2cd-a570-4afb-8a66-177d3d613f81", 1, "DHL Home Delivery", "dhl", 100, "", "", "");
            var shippingOption2 = new ShippingOption("875fb2cd-a570-4afb-8a66-177d3d613f81", 1, "DHL Home Delivery", "dhl", 50, "", "", "");

            var allData = new List<object[]>
            {
                new object[] { new List<OrderRow> { orderRow1, orderRow2, orderRow3, orderRow4 }, shippingOption1, 21.26M },
                new object[] { new List<OrderRow> { orderRow1, orderRow2 }, shippingOption1, 80M * 0.25M + 20M * 0.06M },
                new object[] { new List<OrderRow> { orderRow1, orderRow2 }, shippingOption2, 40M * 0.25M + 10M * 0.06M }
            };

            return allData.Take(numTests);
        }
    }
}