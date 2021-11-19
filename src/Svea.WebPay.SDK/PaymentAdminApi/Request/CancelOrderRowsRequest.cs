namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using System.Text.Json.Serialization;

    public class CancelOrderRowsRequest : IConfigurableAwait
    {
        /// <summary>
        /// CancelOrderRowsRequest
        /// </summary>
        /// <param name="orderRowIds">Id of the rows that will be cancelled.</param>
        /// <param name="configureAwait">true to attempt to marshal the continuation back to the original context captured; otherwise, false.</param>
        public CancelOrderRowsRequest(long[] orderRowIds, bool configureAwait = false)
        {
            OrderRowIds = orderRowIds;
            ConfigureAwait = configureAwait;
        }

        public long[] OrderRowIds { get; }

        [JsonIgnore]
        public bool ConfigureAwait { get; }
    }
}
