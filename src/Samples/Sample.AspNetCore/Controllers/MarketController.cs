namespace Sample.AspNetCore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Sample.AspNetCore.Models;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MarketController : Controller
    {
        private readonly ILogger<MarketController> logger;
        private readonly Market marketService;
        private readonly List<MarketSettings> markets;

        public MarketController(ILogger<MarketController> logger, Market marketService, IOptions<List<MarketSettings>> markets)
        {
            this.logger = logger;
            this.marketService = marketService;
            this.markets = markets.Value;
        }

        public async Task<IActionResult> SetMarket(string marketId)
        {
            var selectedMarket = markets.FirstOrDefault(x => x.Id.Equals(marketId, StringComparison.InvariantCultureIgnoreCase));
            if (selectedMarket != null)
            {
                this.marketService.SetMarket(marketId);
                this.logger.LogInformation($"Market changed to {marketId}");
                if (!selectedMarket.Languages.Contains(marketService.LanguageId))
                {
                    marketService.SetLanguage(selectedMarket.Languages.FirstOrDefault());
                }

                if (!selectedMarket.Currencies.Contains(marketService.CurrencyCode))
                {
                    marketService.SetCurrency(selectedMarket.Currencies.FirstOrDefault());
                }

                this.marketService.Update();
            }
            
            return await Task.FromResult(Redirect(Request.Headers["Referer"].ToString()));
        }
    }
}
