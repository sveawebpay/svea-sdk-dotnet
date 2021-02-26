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

        /// <summary>
        /// The Uri of the completed resource
        /// </summary>
        public Uri ResourceUri { get; }

        /// <summary>
        /// The Uri of the Task
        /// </summary>
        public Uri TaskUri { get; }

        /// <summary>
        /// The completed resource
        /// </summary>
        public T Resource { get; }
    }
}
