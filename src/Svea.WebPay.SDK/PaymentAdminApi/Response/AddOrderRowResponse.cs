namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using Newtonsoft.Json;

    public class AddOrderRowResponse 
    {
        public AddOrderRowResponse() { }
        
        [JsonConstructor]
        public AddOrderRowResponse(AddOrderRowResponseObject responseObject)
        {
            OrderRowId = responseObject.OrderRowId;
        }

        public long[] OrderRowId { get; }
    }
}
