using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svea.WebPay.SDK.Tests.Helpers.DataSamples
{
    public static class UtilityDataSamples
    {
        public static string GetPartPaymentCampaigns = @"[
  {
    ""CampaignCode"": 25,
    ""Description"": ""sample string 26"",
    ""PaymentPlanType"": 0,
    ""ContractLengthInMonths"": 27,
    ""MonthlyAnnuityFactor"": 28.0,
    ""InitialFee"": 29.0,
    ""NotificationFee"": 30.0,
    ""InterestRatePercent"": 31.0,
    ""NumberOfInterestFreeMonths"": 32,
    ""NumberOfPaymentFreeMonths"": 33,
    ""FromAmount"": 34.0,
    ""ToAmount"": 35.0,
    ""MonthlyAmount"": 188.00
  },
  {
    ""CampaignCode"": 25,
    ""Description"": ""sample string 26"",
    ""PaymentPlanType"": 0,
    ""ContractLengthInMonths"": 27,
    ""MonthlyAnnuityFactor"": 28.0,
    ""InitialFee"": 29.0,
    ""NotificationFee"": 30.0,
    ""InterestRatePercent"": 31.0,
    ""NumberOfInterestFreeMonths"": 32,
    ""NumberOfPaymentFreeMonths"": 33,
    ""FromAmount"": 34.00,
    ""ToAmount"": 35.00,
    ""MonthlyAmount"": 188.00
  }
]";
    }
}
