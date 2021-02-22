namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using System.Text.Json.Serialization;

    public class CreditResponse
    {
        public CreditResponse() { }

        [JsonConstructor]
        internal CreditResponse(CreditResponseObject creditResponseObject)
        {
            CreditId = creditResponseObject.CreditId;
        }

        [JsonInclude]
        public string CreditId { get; }
    }
}
