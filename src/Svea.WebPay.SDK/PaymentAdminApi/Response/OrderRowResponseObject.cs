namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using Newtonsoft.Json;

    using System.Collections.Generic;

    public class OrderRowResponseObject : OrderRowBase
    {
        [JsonConstructor]
        internal OrderRowResponseObject(string articleNumber, string name, MinorUnit quantity, MinorUnit unitPrice, MinorUnit discountAmount,
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

        internal int OrderRowId { get; }
        internal bool IsCancelled { get; }
        internal IList<string> Actions { get; }
    }
}