using Microsoft.AspNetCore.Http.HttpResults;
using Svea.WebPay.SDK;
using Svea.WebPay.SDK.CheckoutApi;
using System.Globalization;


namespace Sample.Testshop.Server.Modules.Checkout
{
    public class UpdateOrderHandler
    {
        public static async Task<Results<Ok<Data>, BadRequest<dynamic>>> Handle(long orderId, UpdateOrderRequestModel updateOrderModel, SveaWebPayClient sveaClient)
        {
            if (orderId == 0)
            {
                return TypedResults.BadRequest<dynamic>(new { Message = "OrderId must be provided" });
            }
            var response = await sveaClient.Checkout.UpdateOrder(orderId, new UpdateOrderModel(updateOrderModel.Cart.ToSDKModel()));

            return TypedResults.Ok(response);

        }

        public class UpdateOrderRequestModel
        {
            public Cart Cart { get; set; } = new Cart();
            public string MerchantData { get; set; } = string.Empty;
            public ShippingInformation? ShippingInformation { get; set; }
            public Validation? Validation { get; set; }
        }
        public class Cart
        {
            public List<OrderRow> Items { get; set; } = new List<OrderRow>();
            public Svea.WebPay.SDK.CheckoutApi.Cart ToSDKModel()
            {
                return new Svea.WebPay.SDK.CheckoutApi.Cart(
                    Items.Select(x => new Svea.WebPay.SDK.CheckoutApi.OrderRow(
                        x.ArticleNumber,
                        x.Name,
                        new MinorUnit(x.Quantity),
                        new MinorUnit(x.UnitPrice),
                        new MinorUnit(x.DiscountPercent),
                        new MinorUnit(x.DiscountAmount),
                        new MinorUnit(x.VatPercent),
                        x.Unit,
                        x.TemporaryReference,
                        x.RowNumber,
                        x.MerchantData,
                        x.RowType)).ToList());
            }
        }

        public class OrderRow
        {
            public string? ArticleNumber { get; set; }
            public string Name { get; set; } = string.Empty;
            public long Quantity { get; set; }
            public long UnitPrice { get; set; }
            public long DiscountPercent { get; set; }
            public long DiscountAmount { get; set; }
            public long VatPercent { get; set; }
            public string Unit { get; set; } = string.Empty;
            public string? TemporaryReference { get; set; }
            public int RowNumber { get; set; }
            public string? MerchantData { get; set; }
            public string? RowType { get; set; }
        }
        public class Validation
        {
        }
        public class ShippingInformation
        {
            public bool EnableShipping { get; set; }
            public bool EnforceFallback { get; set; }
            public double Weight { get; set; }
            public Dictionary<string, string>? Tags { get; set; }
            public List<FallbackOption>? FallbackOptions { get; set; }
            public bool ShouldRejectShippingSession { get; set; }
        }

    }
}
