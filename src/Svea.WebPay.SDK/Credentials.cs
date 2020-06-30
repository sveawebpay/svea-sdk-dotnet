using System;

namespace Svea.WebPay.SDK
{
    public class Credentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Credentials"/> class.
        /// </summary>
        /// <param name="merchantId">The checkout merchant identifier.</param>
        /// <param name="secret">The checkout secret.</param>
        public Credentials(string merchantId, string secret)
        {
            if (string.IsNullOrWhiteSpace(merchantId))
            {
                throw new ArgumentNullException(nameof(merchantId));
            }

            if (string.IsNullOrWhiteSpace(secret))
            {
                throw new ArgumentNullException(nameof(secret));
            }


            MerchantId = merchantId;
            Secret = secret;
        }

        /// <summary>
        /// Gets the checkout merchant identifier.
        /// </summary>
        /// <value>
        /// The merchant identifier assigned to you by Svea, formatted as "123456".
        /// </value>
        public string MerchantId { get; }

        /// <summary>
        /// Gets the checkout secret.
        /// </summary>
        /// <value>
        /// Secret key assigned to your CheckoutMerchantId by Svea
        /// </value>
        public string Secret { get; }
    }
}
