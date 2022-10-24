namespace Svea.WebPay.SDK.CheckoutApi
{
    using System;
    using System.Text.Json.Serialization;

    public class OrderRow : OrderRowBase
    {
        /// <summary>
        /// </summary>
        /// <param name="articleNumber">
        /// Article number as a string, can contain letters and numbers.
        /// <remarks>Max length: 256. Min length: 0.</remarks>
        /// </param>
        /// <param name="name">
        /// Article name.
        /// <remarks>Max length: 40. Min length: 0.</remarks>
        /// </param>
        /// <param name="quantity">
        /// Quantity of the product. 1-9 digits.
        /// <remarks>Required</remarks>
        /// </param>
        /// <param name="unitPrice">
        /// Price of the product including VAT. 1-13 digits, can be negative.
        /// <remarks>Required</remarks>
        /// </param>
        /// <param name="discount">
        /// The discount of the product. Can be amount or percent based on the use of useDiscountPercent.
        /// </param>
        /// <param name="vatPercent">
        /// The VAT percentage of the current product. Valid vat percentage for that country.
        /// <remarks>SE 6, 12, 25</remarks>
        ///  </param>
        /// <param name="unit">
        /// The unit type, e.g., “st”, “pc”, “kg” etc.
        /// <remarks>Max length: 4. Min length: 0.</remarks>
        /// </param>
        /// <param name="temporaryReference">
        /// Can be used when creating or updating an order. The returned rows will have their corresponding temporaryReference as they were given in the indata.
        /// It will not be stored and will not be returned in GetOrder.
        /// </param>
        /// <param name="rowNumber">
        /// The row number the row will have in the Webpay system
        /// </param>
        /// <param name="merchantData">
        /// Metadata visible to the store
        /// <remarks>Max length: 255. Optional. Cleaned up from Checkout database after 45 days.</remarks>
        /// </param>
        /// <param name="useDiscountPercent">Set to true if using percent in discount</param>
        /// <param name="rowType">Is used just to distinguish ShippingFee item from the order items. Can be one of "Row" or "ShippingFee"</param>
        public OrderRow(string articleNumber, string name, MinorUnit quantity, MinorUnit unitPrice, MinorUnit discount,
            MinorUnit vatPercent, string unit, string temporaryReference, int rowNumber, string merchantData = null, bool useDiscountPercent = false, RowType rowType = CheckoutApi.RowType.Row)
        {
            ArticleNumber = articleNumber;
            Unit = unit;
            TemporaryReference = temporaryReference;
            RowNumber = rowNumber;
            MerchantData = merchantData;
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

            if (MerchantData?.Length > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(merchantData), "Can only be 0-255 characters.");
            }

            RowType = rowType.ToString();
        }

        /// <summary>
        /// </summary>
        /// <param name="articleNumber">
        /// Article number as a string, can contain letters and numbers.
        /// <remarks>Max length: 256. Min length: 0.</remarks>
        /// </param>
        /// <param name="name">
        /// Article name.
        /// <remarks>Max length: 40. Min length: 0.</remarks>
        /// </param>
        /// <param name="quantity">
        /// Quantity of the product. 1-9 digits.
        /// <remarks>Required</remarks>
        /// </param>
        /// <param name="unitPrice">
        /// Price of the product including VAT. 1-13 digits, can be negative.
        /// <remarks>Required</remarks>
        /// </param>
        /// <param name="discountPercent">
        /// The discount percent of the product. Use this OR discountAmount
        /// </param>
        /// <param name="discountAmount">
        /// The discount amount of the product. Use this OR discountPercent
        /// </param>
        /// <param name="vatPercent">
        /// The VAT percentage of the current product. Valid vat percentage for that country.
        /// <remarks>SE 6, 12, 25</remarks>
        ///  </param>
        /// <param name="unit">
        /// The unit type, e.g., “st”, “pc”, “kg” etc.
        /// <remarks>Max length: 4. Min length: 0.</remarks>
        /// </param>
        /// <param name="temporaryReference">
        /// Can be used when creating or updating an order. The returned rows will have their corresponding temporaryReference as they were given in the indata.
        /// It will not be stored and will not be returned in GetOrder.
        /// </param>
        /// <param name="rowNumber">
        /// The row number the row will have in the Webpay system
        /// </param>
        /// <param name="merchantData">
        /// Metadata visible to the store
        /// <remarks>Max length: 255. Optional. Cleaned up from Checkout database after 45 days.</remarks>
        /// </param>
        [JsonConstructor]
        public OrderRow(string articleNumber, string name, MinorUnit quantity, MinorUnit unitPrice, MinorUnit discountPercent, MinorUnit discountAmount,
           MinorUnit vatPercent, string unit, string temporaryReference, int rowNumber, string merchantData = null, string rowType = "Row")
        {
            ArticleNumber = articleNumber;
            Unit = unit;
            TemporaryReference = temporaryReference;
            RowNumber = rowNumber;
            MerchantData = merchantData;
            DiscountPercent = discountPercent;
            DiscountAmount = discountAmount;

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

            if (DiscountAmount != null && DiscountAmount != 0)
            {
                if (DiscountAmount < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(discountAmount), "Value cannot be less than zero.");
                }

                if (DiscountAmount > unitPrice * quantity)
                {
                    throw new ArgumentOutOfRangeException(nameof(discountAmount), "Value cannot be greater than unit price * quantity.");
                }
            }

            if (DiscountPercent != null && DiscountPercent.InLowestMonetaryUnit > 10000)
            {
                throw new ArgumentOutOfRangeException(nameof(discountPercent), "Value cannot be more than 100%.");
            }

            if (Name.Length < 1 || Name.Length > 40)
            {
                throw new ArgumentOutOfRangeException(nameof(name), "Can only be 1-40 characters.");
            }

            if (Unit?.Length > 4)
            {
                throw new ArgumentOutOfRangeException(nameof(unit), "Can only be 0-4 characters.");
            }

            if (MerchantData?.Length > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(merchantData), "Can only be 0-255 characters.");
            }

            RowType = rowType;
        }

        /// <summary>
        /// Can be used when creating or updating an order. The returned rows will have their corresponding temporaryreference as they were given in the indata.
        /// It will not be stored and will not be returned in GetOrder.
        /// </summary>
        public string TemporaryReference { get; }

        /// <summary>
        /// Metadata visible to the store
        /// </summary>
        /// <remarks>Max length: 255. Optional. Cleaned up from Checkout database after 45 days.</remarks>
        public string MerchantData { get; }
    }
}