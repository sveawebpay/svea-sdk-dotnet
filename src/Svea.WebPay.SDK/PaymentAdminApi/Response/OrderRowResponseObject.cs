
namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class OrderRowResponseObject : OrderRowBase
    {
        [JsonConstructor]
        public OrderRowResponseObject(string articleNumber, string name, MinorUnit quantity, MinorUnit unitPrice, MinorUnit discountAmount, MinorUnit discountPercent,
            MinorUnit vatPercent, string unit, bool isCancelled, int orderRowId, IList<string> actions)
        {
            ArticleNumber = articleNumber;
            Name = name;
            Quantity = quantity;
            UnitPrice = unitPrice;
            DiscountAmount = discountAmount;
            DiscountPercent = discountPercent;
            VatPercent = vatPercent;
            Unit = unit;
            IsCancelled = isCancelled;
            OrderRowId = orderRowId;
            Actions = actions;
        }

        /// <summary>
        /// The identifier of the order row
        /// </summary>
        [JsonInclude]
        public int OrderRowId { get; }

        /// <summary>
        /// Determines if the row is cancelled.
        /// </summary>
        [JsonInclude]
        public bool IsCancelled { get; }

        /// <summary>
        /// A list of actions possible on the order row. See list of OrderRow actions below.
        /// </summary>
        [JsonInclude]
        public IList<string> Actions { get; }
    }
}