namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using Newtonsoft.Json;

    public class CreditResponse
    {
        public CreditResponse() { }

        [JsonConstructor]
        internal CreditResponse(CreditResponseObject creditResponseObject)
        {
            CreditId = creditResponseObject.CreditId;
        }

        public string CreditId { get; }
    }
}
