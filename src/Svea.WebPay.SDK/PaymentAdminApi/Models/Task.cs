namespace Svea.WebPay.SDK.PaymentAdminApi.Models
{
    using Newtonsoft.Json;

    using Svea.WebPay.SDK.PaymentAdminApi.Response;

    using System;

    public class Task : IResourceResponse
    {
        public Task() {}

        [JsonConstructor]
        public Task(long id, string status)
        {
            Id = id;
            Status = status;
        }

        public long Id { get; }

        public string Status { get; }

        public Uri ResourceUri { get; set; }
    }
}
