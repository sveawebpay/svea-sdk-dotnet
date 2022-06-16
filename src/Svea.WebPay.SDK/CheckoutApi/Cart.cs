using System.Collections.Generic;
using System.Linq;

namespace Svea.WebPay.SDK.CheckoutApi
{
    public class Cart
    {
        public Cart(IList<OrderRow> items)
        {
            Items = items;
        }

        public IList<OrderRow> Items { get; }


        public IList<OrderRow> CalculateShippingOrderRows(IShippingOption shippingOption)
        {
            var shippingOrderRows = new List<OrderRow>();

            var totalSum = Items.Sum(x => x.Quantity * x.UnitPrice - x.DiscountAmount);
            var taxGroup = Items.GroupBy(x => x.VatPercent);
            
            var rowNumber = Items.Max(x => x.RowNumber);
            foreach (var tax in taxGroup)
            {
                rowNumber++;
                var sumOnTaxValue = tax.Sum(x => x.Quantity * x.UnitPrice - x.DiscountAmount);
                var percentageOnTaxValue = sumOnTaxValue / totalSum;

                var shippingPrice = percentageOnTaxValue * shippingOption.Price;

                var price = new MinorUnit(shippingPrice);
                var discount = new MinorUnit(0);
                shippingOrderRows.Add(new OrderRow($"{shippingOption.Carrier}_{tax.Key}", $"{shippingOption.Carrier} VAT: {tax.Key}%", new MinorUnit(100), price, discount, tax.Key, "st", null, rowNumber, rowType: RowType.ShippingFee));
            }

            return shippingOrderRows;
        }
    }
}