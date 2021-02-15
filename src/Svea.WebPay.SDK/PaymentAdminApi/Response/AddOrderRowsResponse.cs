namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using System.Text.Json.Serialization;

    public class AddOrderRowsResponse
    {
        public AddOrderRowsResponse() { }
        
        [JsonConstructor]
        public AddOrderRowsResponse(AddOrderRowsResponseObject responseObject)
        {
            OrderRowId = responseObject.OrderRowId;
        }

        [JsonInclude]
        public long[] OrderRowId { get; }
    }
}
