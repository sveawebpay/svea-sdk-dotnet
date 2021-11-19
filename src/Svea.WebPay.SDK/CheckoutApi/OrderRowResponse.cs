namespace Svea.WebPay.SDK.CheckoutApi
{
    using System;
    using System.Text.Json.Serialization;

    public class OrderRowResponse : OrderRowBase
    {
        [JsonConstructor]
        public OrderRowResponse(string articleNumber, string name, MinorUnit quantity, MinorUnit unitPrice, MinorUnit discountPercent, MinorUnit discountAmount,
            MinorUnit vatPercent, string unit, string temporaryReference, int rowNumber, string merchantData = null)
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
        }

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