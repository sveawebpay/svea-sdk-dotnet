using Sample.AspNetCore.Models;
using System.Collections.Generic;
using Svea.WebPay.SDK;
using Svea.WebPay.SDK.CheckoutApi;

namespace Sample.AspNetCore.Extensions
{
    public static class CartLineExtensions
    {
        public static IEnumerable<OrderRow> ToOrderItems(this IEnumerable<CartLine> lines)
        {
            var rowNumber = 1;
            foreach (var line in lines)
            {
                yield return new OrderRow(
                    line.Product.Reference,
                    line.Product.Name,
                    MinorUnit.FromDecimal(line.Quantity),
                    MinorUnit.FromDecimal(line.Product.Price),
                    MinorUnit.FromDecimal(line.Product.DiscountAmount),
                    MinorUnit.FromDecimal(line.Product.VatPercentage),
                    null,
                    null,
                    rowNumber++);
            }
        }
    }
}