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
                    new MinorUnit(line.Quantity),
                    new MinorUnit(line.Product.Price),
                    new MinorUnit(line.Product.DiscountAmount),
                    new MinorUnit(line.Product.VatPercentage),
                    null,
                    null,
                    rowNumber++);
            }
        }
    }
}