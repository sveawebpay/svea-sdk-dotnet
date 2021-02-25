namespace Svea.WebPay.SDK.PaymentAdminApi.Models
{
    using System;

    public class Fee
    {
        /// <summary>
        /// </summary>
        /// <param name="articleNumber">Article number as a string. Can contain letters and numbers.
        /// <remarks>Maximum 256 characters.</remarks>
        /// </param>
        /// <param name="name">Article name.
        /// <remarks>1-40 characters.</remarks>
        /// </param>
        /// <param name="unitPrice">Price of the product including VAT.
        /// <remarks>1-13 digits, should not be negative. Minor currency.</remarks>
        /// </param>
        /// <param name="vatPercent">The VAT percentage of the current product.
        /// <remarks>Allowed valid vat percentage for different countries are available in Data objects chapter.</remarks>
        /// </param>
        public Fee(string articleNumber, string name, MinorUnit unitPrice, MinorUnit vatPercent)
        {
            ArticleNumber = articleNumber;

            Name = name ?? throw new ArgumentNullException(nameof(name));
            UnitPrice = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice));
            VatPercent = vatPercent ?? throw new ArgumentNullException(nameof(vatPercent));

            if (ArticleNumber?.Length > 256)
            {
                throw new ArgumentOutOfRangeException(nameof(articleNumber), "Maximum 256 characters.");
            }

            if (Name.Length < 1 || Name.Length > 40)
            {
                throw new ArgumentOutOfRangeException(nameof(name), "Can only be 1-40 characters.");
            }

            if (UnitPrice.InLowestMonetaryUnit.ToString().Length < 1 || UnitPrice.InLowestMonetaryUnit.ToString().Length > 13)
            {
                throw new ArgumentOutOfRangeException(nameof(UnitPrice), "Can only be 1-13 characters.");
            }
        }

        /// <summary>
        /// Article number as a string. Can contain letters and numbers.
        /// </summary>
        public string ArticleNumber { get; }

        /// <summary>
        /// Article name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Price of the product including VAT.
        /// </summary>
        public MinorUnit UnitPrice { get; }

        /// <summary>
        /// The VAT percentage of the current product.
        /// </summary>
        public MinorUnit VatPercent { get; }
    }
}
