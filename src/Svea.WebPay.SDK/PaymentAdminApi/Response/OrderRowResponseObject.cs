namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using Newtonsoft.Json;

    using System.Collections.Generic;

    public class OrderRowResponseObject
    {
        [JsonConstructor]
        internal OrderRowResponseObject(string articleNumber, string name, MinorUnit quantity, MinorUnit unitPrice, MinorUnit discountPercent,
            MinorUnit vatPercent, string unit, bool isCancelled, int orderRowId, IList<string> actions)
        {
            ArticleNumber = articleNumber;
            Name = name;
            Quantity = quantity;
            UnitPrice = unitPrice;
            DiscountPercent = discountPercent;
            VatPercent = vatPercent;
            Unit = unit;
            IsCancelled = isCancelled;
            OrderRowId = orderRowId;
            Actions = actions;
        }

        internal int OrderRowId { get; }
        internal string ArticleNumber { get; }
        internal string Name { get; }
        internal MinorUnit Quantity { get; }
        internal MinorUnit UnitPrice { get; }
        internal MinorUnit DiscountPercent { get; }
        internal MinorUnit VatPercent { get; }
        internal string Unit { get; }
        internal bool IsCancelled { get; }
        internal IList<string> Actions { get; }
    }
}