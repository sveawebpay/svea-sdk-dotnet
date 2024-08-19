
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sample.Testshop.Server.Configurations;

namespace Sample.Testshop.Server.Modules.Checkout
{
    public class GetMerchantsHandler
    {
        public static dynamic Handle(IOptions<List<Credentials>> options )
        {
            var credentials = options?.Value;
            return credentials?.Select(x => new { Market = x.MarketId, MerchantId = x.MerchantId });
        }
    }
}
