namespace Svea.WebPay.SDK.CheckoutApi
{
    using System;

    public class OrderRow
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleNumber">
        /// <summary>
        /// Articlenumber as a string, can contain letters and numbers.
        /// </summary>
        /// <remarks>Max length: 256. Min length: 0.</remarks>
        /// </param>
        /// <param name="name">
        /// <summary>
        /// Article name.
        /// </summary>
        /// <remarks>Max length: 40. Min length: 0.</remarks>
        /// </param>
        /// <param name="quantity">
        /// <summary>
        /// Quantity of the product. 1-9 digits.
        /// </summary>
        /// <remarks>Required</remarks>
        /// </param>
        /// <param name="unitPrice">
        /// <summary>
        /// Price of the product including VAT. 1-13 digits, can be negative.
        /// </summary>
        /// <remarks>Required</remarks>
        /// </param>
        /// <param name="discountPercent">
        /// <summary>
        /// The discount percent of the product. Examples: 0-10000. No fractions.0 = 0%100 = 1%1000 = 10%9900 = 99%10000 = 100%
        /// </summary>
        /// </param>
        /// <param name="vatPercent">
        /// <summary>
        /// The VAT percentage of the current product. Valid vat percentage for that country.
        /// </summary>
        /// <remarks>SE 6, 12, 25</remarks>
        ///  </param>
        /// <param name="unit">
        /// <summary>
        /// The unit type, e.g., “st”, “pc”, “kg” etc.
        /// </summary>
        /// <remarks>Max length: 4. Min length: 0.</remarks>
        /// </param>
        /// <param name="temporaryReference">
        /// <summary>
        /// Can be used when creating or updating an order. The returned rows will have their corresponding temporaryreference as they were given in the indata.
        /// It will not be stored and will not be returned in GetOrder.
        /// </summary>
        /// </param>
        /// <param name="rowNumber">
        /// <summary>
        /// The row number the row will have in the Webpay system
        /// </summary>
        /// </param>
        /// <param name="merchantData">
        /// <summary>
        /// Metadata visible to the store
        /// </summary>
        /// <remarks>Max length: 255. Optional. Cleaned up from Checkout database after 45 days.</remarks>
        /// </param>
        public OrderRow(string articleNumber, string name, MinorUnit quantity, MinorUnit unitPrice, MinorUnit discountPercent,
            MinorUnit vatPercent, string unit, string temporaryReference, int rowNumber, string merchantData = null)
        {
            ArticleNumber = articleNumber;
            DiscountPercent = discountPercent;
            Unit = unit;
            TemporaryReference = temporaryReference;
            RowNumber = rowNumber;
            MerchantData = merchantData;

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

            if (MerchantData?.Length > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(merchantData), "Can only be 0-255 characters.");
            }
        }

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
        /// The discount percent of the product. Examples: 0-10000. No fractions.0 = 0%100 = 1%1000 = 10%9900 = 99%10000 = 100%
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
        /// Can be used when creating or updating an order. The returned rows will have their corresponding temporaryreference as they were given in the indata.
        /// It will not be stored and will not be returned in GetOrder.
        /// </summary>
        public string TemporaryReference { get; }

        /// <summary>
        /// The row number the row will have in the Webpay system
        /// </summary>
        public int RowNumber { get; }

        /// <summary>
        /// Metadata visible to the store
        /// </summary>
        /// <remarks>Max length: 255. Optional. Cleaned up from Checkout database after 45 days.</remarks>
        public string MerchantData { get; }
    }
}