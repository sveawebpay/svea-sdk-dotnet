namespace Svea.WebPay.SDK.PaymentAdminApi.Request
{
    using System;

    public interface IResourceRequest
    {
        TimeSpan? PollingTimeout { get; }
    }
}
