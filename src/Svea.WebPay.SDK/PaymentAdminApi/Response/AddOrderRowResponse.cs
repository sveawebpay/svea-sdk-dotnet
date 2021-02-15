namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using System.Text.Json.Serialization;

    public class AddOrderRowResponse 
    {
        public AddOrderRowResponse() { }
        
        [JsonConstructor]
        public AddOrderRowResponse(AddOrderRowResponseObject responseObject)
        {
            OrderRowId = responseObject.OrderRowId;
        }

        [JsonInclude]
        public long[] OrderRowId { get; }
    }
}
