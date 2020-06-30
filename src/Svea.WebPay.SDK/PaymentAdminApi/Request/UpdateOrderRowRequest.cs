namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using System;

    public class UpdateOrderRowRequest
    {
        /// <summary>
        /// UpdateOrderRowRequest
        /// </summary>
        /// <param name="articleNumber">Article number as a string. Can contain letters and numbers. Maximum 256 characters.</param>
        /// <param name="name">Article name. 1-40 characters.</param>
        /// <param name="quantity">Quantity of the product.</param>
        /// <param name="unitPrice">Price of the product including VAT.</param>
        /// <param name="discountPercent">The discount percentage of the product.</param>
        /// <param name="vatPercent">The VAT percentage of the credit amount. Valid vat percentage for that country.</param>
        /// <param name="unit">The unit type, e.g., “st”, “pc”, “kg” etc. 0-4 characters.</param>
        public UpdateOrderRowRequest(string articleNumber, string name, MinorUnit quantity, MinorUnit unitPrice, MinorUnit discountPercent, MinorUnit vatPercent, string unit)
        {
            ArticleNumber = articleNumber;
            DiscountPercent = discountPercent;
            Unit = unit;

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
            UnitPrice = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice));
            VatPercent = vatPercent ?? throw new ArgumentNullException(nameof(vatPercent));

            if (ArticleNumber?.Length > 256)
            {
                throw new ArgumentOutOfRangeException(nameof(articleNumber), "Maximum 256 characters.");
            }

            if (DiscountPercent.Value.ToString().Length > 10000)
            {
                throw new ArgumentOutOfRangeException(nameof(discountPercent), "Value cannot be more than 100.");
            }

            if (Quantity.Value.ToString().Length > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Value cannot be longer than 7 digits.");
            }

            if (UnitPrice.Value.ToString().Length > 13)
            {
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "Value cannot be longer than 11 digits.");
            }

            if (Name.Length < 1 || Name.Length > 40)
            {
                throw new ArgumentOutOfRangeException(nameof(name), "Can only be 1-40 characters.");
            }

            if (Unit?.Length > 4)
            {
                throw new ArgumentOutOfRangeException(nameof(unit), "Can only be 0-4 characters.");
            }
        }

        public string ArticleNumber { get; }
        public string Name { get; }
        public MinorUnit Quantity { get; }
        public MinorUnit UnitPrice { get; }
        public MinorUnit DiscountPercent { get; }
        public MinorUnit VatPercent { get; }
        public string Unit { get; }
    }
}
