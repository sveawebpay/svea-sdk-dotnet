namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using System;

    public class ResourceResponseObject<T> : IResourceResponse
    {
        /// <summary>
        /// The Uri of the completed resource
        /// </summary>
        public Uri ResourceUri { get; set; }

        /// <summary>
        /// The Uri of the Task
        /// </summary>
        internal Uri TaskUri { get; set; }

        /// <summary>
        /// The completed resource
        /// </summary>
        internal T Resource { get; set; }
    }
}
