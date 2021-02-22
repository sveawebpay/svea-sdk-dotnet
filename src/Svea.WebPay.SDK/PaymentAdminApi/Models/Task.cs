namespace Svea.WebPay.SDK.PaymentAdminApi.Models
{

    using Svea.WebPay.SDK.PaymentAdminApi.Response;

    using System;
    using System.Text.Json.Serialization;

    public class Task : IResourceResponse
    {
        public Task() {}

        [JsonConstructor]
        public Task(long id, string status)
        {
            Id = id;
            Status = status;
        }
        
        [JsonInclude]
        public long Id { get; }

        [JsonInclude]
        public string Status { get; }

        [JsonInclude]
        public Uri ResourceUri { get; set; }
    }
}
