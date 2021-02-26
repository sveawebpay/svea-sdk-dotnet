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
        
        /// <summary>
        /// Id of the Task
        /// </summary>
        [JsonInclude]
        public long Id { get; }

        /// <summary>
        /// The Status of the Task
        /// </summary>
        [JsonInclude]
        public string Status { get; }

        /// <summary>
        /// The Uri to the completed Resource
        /// </summary>
        [JsonInclude]
        public Uri ResourceUri { get; set; }
    }
}
