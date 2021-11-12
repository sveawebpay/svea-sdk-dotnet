namespace Svea.WebPay.SDK.PaymentAdminApi.Models
{
    using System;
    using System.Text.Json.Serialization;

    public class NewOrderRow : OrderRowBase
    {
        /// <summary>
        /// OrderRowRequest
        /// </summary>
        /// <param name="name">Article name. 1-40 characters.</param>
        /// <param name="quantity">Quantity of the product.</param>
        /// <param name="unitPrice">Price of the product including VAT.</param>
        /// <param name="vatPercent">The VAT percentage of the credit amount. Valid vat percentage for that country.</param>
        /// <param name="discount">The discount of the product. Can be amount or percent based on the use of useDiscountPercent.</param>
        /// <param name="rowId">Id of row to update</param>
        /// <param name="unit">The unit type, e.g., “st”, “pc”, “kg” etc. 0-4 characters.</param>
        /// <param name="articleNumber">Article number as a string. Can contain letters and numbers. Maximum 256 characters.</param>
        /// <param name="useDiscountPercent">Set to true if using percent in discount</param>
        public NewOrderRow(string name, MinorUnit quantity, MinorUnit unitPrice, MinorUnit vatPercent, MinorUnit discount = null, long? rowId = null, string unit = "", string articleNumber = "", bool useDiscountPercent = false)
        {
            ArticleNumber = articleNumber;
            Unit = unit;
            OrderRowId = rowId;
            DiscountPercent = useDiscountPercent ? discount : null;
            DiscountAmount = !useDiscountPercent ? discount : null;

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
            UnitPrice = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice));
            VatPercent = vatPercent ?? throw new ArgumentNullException(nameof(vatPercent));

            if (ArticleNumber?.Length > 256)
            {
                throw new ArgumentOutOfRangeException(nameof(articleNumber), "Maximum 256 characters.");
            }
            
            if (Quantity.InLowestMonetaryUnit.ToString().Length > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Value cannot be longer than 7 digits.");
            }

            if (UnitPrice.InLowestMonetaryUnit.ToString().Length > 13)
            {
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "Value cannot be longer than 11 digits.");
            }

            if (!useDiscountPercent && DiscountAmount != null && DiscountAmount != 0)
            {
                if (DiscountAmount < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(discount), "Value cannot be less than zero.");
                }

                if (DiscountAmount > unitPrice * quantity)
                {
                    throw new ArgumentOutOfRangeException(nameof(discount), "Value cannot be greater than unit price * quantity.");
                }
            }

            if (useDiscountPercent && DiscountPercent != null && DiscountPercent.InLowestMonetaryUnit > 10000)
            {
                throw new ArgumentOutOfRangeException(nameof(discount), "Value cannot be more than 100%.");
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

        public long? OrderRowId { get; }
    }
}
