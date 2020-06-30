namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using Newtonsoft.Json;

    public class AddOrderRowResponseObject
    {
        public AddOrderRowResponseObject() { }
        
        [JsonConstructor]
        internal AddOrderRowResponseObject(long[] orderRowId)
        {
            OrderRowId = orderRowId;
        }

        internal long[] OrderRowId { get; }
    }
}
