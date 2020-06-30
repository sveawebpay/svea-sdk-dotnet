namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using System;

    public class CreditAmountRequest
    {
        /// <summary>
        /// CreditAmountRequest
        /// </summary>
        /// <param name="creditedAmount">To make a credit on a delivery, specify a credited amount larger than the current credited amount. The credited amount cannot be lower than the current credited amount or larger than the delivered amount.</param>
        public CreditAmountRequest(MinorUnit creditedAmount)
        {
            CreditedAmount = creditedAmount ?? throw new ArgumentNullException(nameof(creditedAmount));
        }

        public MinorUnit CreditedAmount { get; }
    }
}
