using System;

namespace Svea.WebPay.SDK.PaymentAdminApi
{
    /// <summary>
    /// The timeout of the polling request. If null the resource will be returned if complete, otherwise the task is returned.
    /// </summary>
    public class PollingTimeout
    {
        /// <summary>
        /// The timeout of the polling request
        /// </summary>
        public TimeSpan Timeout { get; }

        /// <summary>
        /// The timeout of the polling request. If null the resource will be returned if complete, otherwise the task is returned.
        /// </summary>
        /// <param name="seconds">The amount of seconds the resource will be polled.</param>
        public PollingTimeout(int seconds = 60)
        {
            Timeout = TimeSpan.FromSeconds(seconds);
        }
    }
}
