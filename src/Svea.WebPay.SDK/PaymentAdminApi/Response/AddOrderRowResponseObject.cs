namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using System.Text.Json.Serialization;

    public class AddOrderRowResponseObject
    {
        public AddOrderRowResponseObject() { }
        
        [JsonConstructor]
        public AddOrderRowResponseObject(long[] orderRowId)
        {
            OrderRowId = orderRowId;
        }

        [JsonInclude]
        public long[] OrderRowId { get; }
    }
}
