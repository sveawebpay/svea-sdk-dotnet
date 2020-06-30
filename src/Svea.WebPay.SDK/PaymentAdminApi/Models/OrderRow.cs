namespace Svea.WebPay.SDK.PaymentAdminApi.Models
{
    using Svea.WebPay.SDK.PaymentAdminApi.Response;

    using System.Collections.Generic;

    public class OrderRow
    {
        public OrderRow(long orderId, OrderRowResponseObject orderRowResponse, SveaHttpClient client) 
        { 
            Actions = new OrderRowActions(orderId, orderRowResponse, client);
            OrderRowId = orderRowResponse.OrderRowId;
            ArticleNumber = orderRowResponse.ArticleNumber;
            Name = orderRowResponse.Name;
            Quantity = orderRowResponse.Quantity;
            UnitPrice = orderRowResponse.UnitPrice;
            DiscountPercent = orderRowResponse.DiscountPercent;
            VatPercent = orderRowResponse.VatPercent;
            Unit = orderRowResponse.Unit;
            IsCancelled = orderRowResponse.IsCancelled;
            AvailableActions = orderRowResponse.Actions;
        }

        public IList<string> AvailableActions { get; set; }

        public OrderRowActions Actions { get; }


        public int OrderRowId { get; }

        /// <summary>
        /// Articlenumber as a string, can contain letters and numbers.
        /// </summary>
        /// <remarks>Max length: 256. Min length: 0.</remarks>
        public string ArticleNumber { get; }

        /// <summary>
        /// Article name.
        /// </summary>
        /// <remarks>Max length: 40. Min length: 0.</remarks>
        public string Name { get; }

        /// <summary>
        /// Quantity of the product. 1-9 digits.
        /// </summary>
        /// <remarks>Required</remarks>
        public MinorUnit Quantity { get; }

        /// <summary>
        /// Price of the product including VAT. 1-13 digits, can be negative.
        /// </summary>
        /// <remarks>Required</remarks>
        public MinorUnit UnitPrice { get; }

        /// <summary>
        /// The discount percent of the product
        /// </summary>
        public MinorUnit DiscountPercent { get; }

        /// <summary>
        /// The VAT percentage of the current product. Valid vat percentage for that country.
        /// </summary>
        public MinorUnit VatPercent { get; }

        /// <summary>
        /// The unit type, e.g., “st”, “pc”, “kg” etc.
        /// </summary>
        /// <remarks>Max length: 4. Min length: 0.</remarks>
        public string Unit { get; }

        /// <summary>
        /// Determines if the row is cancelled.
        /// </summary>
        public bool IsCancelled { get; }
    }
}
