namespace Svea.WebPay.SDK.PaymentAdminApi.Response
{
    using System;

    internal interface IResourceResponse
    {
        /// <summary>
        /// URI to the created resource
        /// </summary>
        Uri ResourceUri { get; set; }
    }
}
