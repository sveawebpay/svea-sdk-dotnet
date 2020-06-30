namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using System;

    public class CancelAmountRequest
    {
        /// <summary>
        /// CancelAmountRequest
        /// </summary>
        /// <param name="cancelledAmount">The new CancelledAmount cannot be equal to or lower than the current CancelledAmount. Neither can it be higher than the total order amount.</param>
        public CancelAmountRequest(MinorUnit cancelledAmount)
        {
            CancelledAmount = cancelledAmount ?? throw new ArgumentNullException(nameof(cancelledAmount));
        }

        public MinorUnit CancelledAmount { get; }
    }
}
