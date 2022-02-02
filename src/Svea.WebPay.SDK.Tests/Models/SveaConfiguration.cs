using System.Collections.Generic;

namespace Svea.WebPay.SDK.Tests.Models
{
    public class SveaConfiguration
    {
        public SveaApiUrls SveaApiUrls { get; set; }
        public IEnumerable<Credentials> Credentials { get; set; }
        public MerchantSettings MerchantSettings { get; set; }
    }
}
