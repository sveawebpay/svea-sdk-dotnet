namespace Svea.WebPay.SDK
{
    using System.Text.Json.Serialization;

    public class OrderRowBase
    {
        /// <summary>
        /// Article number as a string, can contain letters and numbers.
        /// </summary>
        /// <remarks>Max length: 256. Min length: 0.</remarks>
        public string ArticleNumber { get; internal set; }

        /// <summary>
        /// Article name.
        /// </summary>
        /// <remarks>Max length: 40. Min length: 0.</remarks>
        public string Name { get; internal set; }

        /// <summary>
        /// Quantity of the product. 1-9 digits.
        /// </summary>
        /// <remarks>Required</remarks>
        public MinorUnit Quantity { get; internal set; }

        /// <summary>
        /// Price of the product including VAT. 1-13 digits, can be negative.
        /// </summary>
        /// <remarks>Required</remarks>
        public MinorUnit UnitPrice { get; internal set; }

        /// <summary>
        /// The discount amount of the product.
        /// </summary>
        public MinorUnit DiscountAmount { get; internal set; }

        /// <summary>
        /// The discount percent of the product.
        /// </summary>
        public MinorUnit DiscountPercent { get; internal set; }

        /// <summary>
        /// The VAT percentage of the current product. Valid vat percentage for that country.
        /// </summary>
        public MinorUnit VatPercent { get; internal set; }

        /// <summary>
        /// The unit type, e.g., “st”, “pc”, “kg” etc.
        /// </summary>
        /// <remarks>Max length: 4. Min length: 0.</remarks>
        public string Unit { get; internal set; }
    }
}
