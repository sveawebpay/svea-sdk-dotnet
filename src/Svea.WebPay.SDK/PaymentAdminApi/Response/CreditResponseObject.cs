namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using Newtonsoft.Json;

    public class CreditResponseObject
    {
        public CreditResponseObject() { }

        [JsonConstructor]
        internal CreditResponseObject(string creditId)
        {
            CreditId = creditId;
        }

        internal string CreditId { get; }
    }
}
