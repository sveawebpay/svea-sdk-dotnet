namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using System.Text.Json.Serialization;

    public class AddOrderRowsResponseObject
    {
        public AddOrderRowsResponseObject() { }
        

        [JsonConstructor]
        public AddOrderRowsResponseObject(long[] orderRowId)
        {
            OrderRowId = orderRowId;
        }

        /// <summary>
        /// The row IDs of the newly created OrderRows.
        /// </summary>
        [JsonInclude]
        public long[] OrderRowId { get; }
    }
}
