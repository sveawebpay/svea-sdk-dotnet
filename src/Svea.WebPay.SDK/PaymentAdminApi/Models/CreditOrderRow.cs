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
        public CreditOrderRow(string name, MinorUnit unitPrice, MinorUnit vatPercent)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            UnitPrice = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice));
            VatPercent = vatPercent ?? throw new ArgumentNullException(nameof(vatPercent));

            if (Name.Length < 1 || Name.Length > 40)
            {
                throw new ArgumentOutOfRangeException(nameof(name), "Can only be 1-40 characters.");
            }

            if (UnitPrice.InLowestMonetaryUnit.ToString().Length > 13)
            {
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "Value cannot be longer than 11 digits.");
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
    }
}
