using Microsoft.AspNetCore.Mvc;
using Svea.WebPay.SDK;

namespace Sample.Testshop.Server.Modules.Checkout
{
    public static class GetAvailableCampaignsHandler
    {
        //public static Task<AvailableCampaigns> Handle(SveaWebPayClient sveaClient, [FromQuery] bool isCompany, [FromQuery] int amount)
        //{   
        //    //sveaClient.Checkout.
        //}
        public class AvailableCampaigns
        {
            public int CampaignCode { get; set; }
            public string Description { get; set; }
            public int PaymentPlanType { get; set; }
            public int ContractLengthInMonths { get; set; }
            public double MonthlyAnnuityFactor { get; set; }
            public double InitialFee { get; set; }
            public double NotificationFee { get; set; }
            public double InterestRatePercent { get; set; }
            public int NumberOfInterestFreeMonths { get; set; }
            public int NumberOfPaymentFreeMonths { get; set; }
            public decimal FromAmount { get; set; }
            public decimal ToAmount { get; set; }
            public decimal MonthlyAmount { get; set; }
        }

    }
}
