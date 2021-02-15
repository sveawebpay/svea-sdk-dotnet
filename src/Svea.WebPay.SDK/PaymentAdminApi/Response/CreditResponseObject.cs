namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using System.Text.Json.Serialization;

    public class CreditResponseObject
    {
        public CreditResponseObject() { }

        [JsonConstructor]
        public CreditResponseObject(string creditId)
        {
            CreditId = creditId;
        }

        [JsonInclude]
        public string CreditId { get; }
    }
}
