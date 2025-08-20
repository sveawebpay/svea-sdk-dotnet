namespace Svea.WebPay.SDK.PaymentAdminApi.Models
{
    using System;

    public class CreditOrderRow
    {
        /// <summary>
        /// CreditOrderRow
        /// </summary>
        /// <param name="name">Credit row name. Credit row name.</param>
        /// <param name="unitPrice">Credit amount including VAT.</param>
        /// <param name="vatPercent">The VAT percentage of the credit amount. Valid vat percentage for that country.</param>
        /// <param name="quantity">Quantity of the product. 1-9 digits. Minor unit. Only positive. Default value is 1.</param>
        /// <param name="articleNumber">Article number of order row./param>
        /// <param name="unit">The unit type, e.g., “st”, “pc”, “kg” etc.</param>
        /// <param name="discountPercent">Discount percent applied to the order row.</param>
        /// <param name="discountAmount">Discount amount in the order row’s currency.</param>
        public CreditOrderRow(string name, MinorUnit unitPrice, MinorUnit vatPercent,string articleNumber=null,string unit=null,MinorUnit discountPercent=null,MinorUnit discountAmount=null ,MinorUnit quantity = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            UnitPrice = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice));
            VatPercent = vatPercent ?? throw new ArgumentNullException(nameof(vatPercent));
            Quantity = quantity ?? 1;
            ArticleNumber = articleNumber;
            Unit = unit;
            DiscountPercent = discountPercent;
            DiscountAmount = discountAmount;

            if (Name.Length < 1 || Name.Length > 40)
            {
                throw new ArgumentOutOfRangeException(nameof(name), "Can only be 1-40 characters.");
            }

            if (!string.IsNullOrEmpty(ArticleNumber) && ArticleNumber.Length > 256)
            {
                throw new ArgumentOutOfRangeException(nameof(articleNumber), "Can only be max. 256 characters.");
            }

            if (UnitPrice.InLowestMonetaryUnit.ToString().Length > 13)
            {
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "Value cannot be longer than 11 digits.");
            }

            if (DiscountPercent != null && DiscountAmount != null)
            {
                throw new InvalidOperationException("Specify either discountPercent or discountAmount, not both.");
            }

            if (DiscountPercent != null && (DiscountPercent < 0 || DiscountPercent > 100))
            {
                throw new ArgumentOutOfRangeException(nameof(discountPercent), "Discount percent must be between 0% and 100%.");
            }

            if (DiscountAmount != null && (DiscountAmount >= (UnitPrice * Quantity)))
            {
                throw new ArgumentOutOfRangeException(nameof(discountAmount), "Discount amount cannot exceed row amount.");
            }
        }

        /// <summary>
        /// Credit row name. Credit row name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Credit amount including VAT.
        /// </summary>
        public MinorUnit UnitPrice { get; }

        /// <summary>
        /// The VAT percentage of the credit amount. Valid vat percentage for that country.
        /// </summary>
        public MinorUnit VatPercent { get; }

        /// <summary>
        /// Quantity of the product. 1-9 digits. Minor unit. Only positive. Default value is 1.
        /// </summary>
        public MinorUnit Quantity { get; }

        /// <summary>
        /// Article number of order row.
        /// </summary>
        public string ArticleNumber { get; }

        /// <summary>
        /// The unit type, e.g., “st”, “pc”, “kg” etc.
        /// </summary>
        public string Unit { get; }

        /// <summary>
        /// Discount percent applied to the order row.
        /// </summary>
        public MinorUnit DiscountPercent { get; }

        /// <summary>
        /// Discount amount in the order row’s currency.
        /// </summary>
        public MinorUnit DiscountAmount { get; }




    }
}
