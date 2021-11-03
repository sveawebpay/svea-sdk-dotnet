namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using System.Text.Json.Serialization;

    public class CancelRequest : IConfigurableAwait
    {
        /// <summary>
        /// CancelRequest
        /// </summary>
        /// <param name="isCancelled">Set to true to cancel order. This cannot be undone</param>
        /// <param name="configureAwait">true to attempt to marshal the continuation back to the original context captured; otherwise, false.</param>
        public CancelRequest(bool isCancelled, bool configureAwait = false)
        {
            IsCancelled = isCancelled;
            ConfigureAwait = configureAwait;
        }

        public bool IsCancelled { get; }

        [JsonIgnore]
        public bool ConfigureAwait { get; }
    }
}
