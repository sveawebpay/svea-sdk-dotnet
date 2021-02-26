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

        /// <summary>
        /// ID to each identify the credit.
        /// </summary>
        [JsonInclude]
        public string CreditId { get; }
    }
}
