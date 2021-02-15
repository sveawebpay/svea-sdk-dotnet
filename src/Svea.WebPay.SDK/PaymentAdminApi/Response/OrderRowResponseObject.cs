namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class OrderRowResponseObject : OrderRowBase
    {
        [JsonConstructor]
        public OrderRowResponseObject(string articleNumber, string name, MinorUnit quantity, MinorUnit unitPrice, MinorUnit discountAmount,
            MinorUnit vatPercent, string unit, bool isCancelled, int orderRowId, IList<string> actions)
        {
            ArticleNumber = articleNumber;
            Name = name;
            Quantity = quantity;
            UnitPrice = unitPrice;
            DiscountAmount = discountAmount;
            VatPercent = vatPercent;
            Unit = unit;
            IsCancelled = isCancelled;
            OrderRowId = orderRowId;
            Actions = actions;
        }

        [JsonInclude]
        public int OrderRowId { get; }

        [JsonInclude]
        public bool IsCancelled { get; }

        [JsonInclude]
        public IList<string> Actions { get; }
    }
}