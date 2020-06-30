namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using System;

    public class ResourceResponseObject<T> : IResourceResponse
    {
        public Uri ResourceUri { get; set; }

        internal Uri TaskUri { get; set; }

        internal T Resource { get; set; }
    }
}
