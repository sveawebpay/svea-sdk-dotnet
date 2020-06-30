namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using Newtonsoft.Json;

    public class AddOrderRowsResponse
    {
        public AddOrderRowsResponse() { }
        
        [JsonConstructor]
        public AddOrderRowsResponse(long[] orderRowId)
        {
            OrderRowId = orderRowId;
        }

        /// <summary>
        /// The row IDs of the newly created OrderRows.
        /// </summary>
        public long[] OrderRowId { get; }
    }
}
