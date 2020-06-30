namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using System;

    public class ResourceResponse<TSource, T>
    {
        public ResourceResponse(ResourceResponseObject<TSource> response, Func<T> delegateObj)
        {
            ResourceUri = response.ResourceUri;
            TaskUri = response.TaskUri;
            Resource = delegateObj();
        }

        public Uri ResourceUri { get; }

        public Uri TaskUri { get; }

        public T Resource { get; }
    }
}
