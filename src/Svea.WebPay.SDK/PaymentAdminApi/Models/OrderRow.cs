namespace Svea.WebPay.SDK.PaymentAdminApi.Models
{
    using Svea.WebPay.SDK.PaymentAdminApi.Response;

    using System.Collections.Generic;

    public class OrderRow : OrderRowBase
    {
        public OrderRow(long orderId, OrderRowResponseObject orderRowResponse, SveaHttpClient client) 
        { 
            Actions = new OrderRowActions(orderId, orderRowResponse, client);
            OrderRowId = orderRowResponse.OrderRowId;
            ArticleNumber = orderRowResponse.ArticleNumber;
            Name = orderRowResponse.Name;
            Quantity = orderRowResponse.Quantity;
            UnitPrice = orderRowResponse.UnitPrice;
            DiscountAmount = orderRowResponse.DiscountAmount;
            DiscountPercent = orderRowResponse.DiscountPercent;
            VatPercent = orderRowResponse.VatPercent;
            Unit = orderRowResponse.Unit;
            IsCancelled = orderRowResponse.IsCancelled;
            AvailableActions = orderRowResponse.Actions;
        }

        public IList<string> AvailableActions { get; }

        public OrderRowActions Actions { get; }

        public long OrderRowId { get; }

        /// <summary>
        /// Determines if the row is cancelled.
        /// </summary>
        public bool IsCancelled { get; }
    }
}
