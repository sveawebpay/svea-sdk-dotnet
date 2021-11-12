namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using System;
    using System.Text.Json.Serialization;

    public class CancelAmountRequest : IConfigurableAwait
    {
        /// <summary>
        /// CancelAmountRequest
        /// </summary>
        /// <param name="cancelledAmount">The new CancelledAmount cannot be equal to or lower than the current CancelledAmount. Neither can it be higher than the total order amount.</param>
        /// <param name="configureAwait">true to attempt to marshal the continuation back to the original context captured; otherwise, false.</param>
        public CancelAmountRequest(MinorUnit cancelledAmount, bool configureAwait = false)
        {
            CancelledAmount = cancelledAmount ?? throw new ArgumentNullException(nameof(cancelledAmount));
            ConfigureAwait = configureAwait;
        }

        public MinorUnit CancelledAmount { get; }

        [JsonIgnore]
        public bool ConfigureAwait { get; }
    }
}
