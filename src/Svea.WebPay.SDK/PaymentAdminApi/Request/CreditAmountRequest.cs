namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using System;
    using System.Text.Json.Serialization;

    public class CreditAmountRequest : IConfigurableAwait
    {
        /// <summary>
        /// CreditAmountRequest
        /// </summary>
        /// <param name="creditedAmount">To make a credit on a delivery, specify a credited amount larger than the current credited amount. The credited amount cannot be lower than the current credited amount or larger than the delivered amount.</param>
        /// <param name="configureAwait">true to attempt to marshal the continuation back to the original context captured; otherwise, false.</param>
        public CreditAmountRequest(MinorUnit creditedAmount, bool configureAwait = false) 
        {
            CreditedAmount = creditedAmount ?? throw new ArgumentNullException(nameof(creditedAmount));
            ConfigureAwait = configureAwait;
        }

        public MinorUnit CreditedAmount { get; }

        [JsonIgnore]
        public bool ConfigureAwait { get; }
    }
}
