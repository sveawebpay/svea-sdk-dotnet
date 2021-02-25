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


        /// <summary>
        /// ID to identify the credit.
        /// </summary>
        [JsonInclude]
        public string CreditId { get; }
    }
}
