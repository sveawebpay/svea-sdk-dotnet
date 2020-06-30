namespace Svea.WebPay.SDK.CheckoutApi
{
    public class CampaignCodeInfo
    {
        public CampaignCodeInfo(long campaignCode, string description, PaymentPlanTypeCode paymentPlanType, int contractLengthInMonths, decimal monthlyAnnuityFactor, decimal initialFee, decimal notificationFee, decimal interestRatePercent, int numberOfInterestFreeMonths, int numberOfPaymentFreeMonths, decimal fromAmount, decimal amount)
        {
            CampaignCode = campaignCode;
            Description = description;
            PaymentPlanType = paymentPlanType;
            ContractLengthInMonths = contractLengthInMonths;
            MonthlyAnnuityFactor = monthlyAnnuityFactor;
            InitialFee = initialFee;
            NotificationFee = notificationFee;
            InterestRatePercent = interestRatePercent;
            NumberOfInterestFreeMonths = numberOfInterestFreeMonths;
            NumberOfPaymentFreeMonths = numberOfPaymentFreeMonths;
            FromAmount = fromAmount;
            ToAmount = amount;
        }

        public long CampaignCode { get; set; }
        public string Description { get; set; }
        public PaymentPlanTypeCode PaymentPlanType { get; set; }
        public int ContractLengthInMonths { get; set; }
        public decimal MonthlyAnnuityFactor { get; set; }
        public decimal InitialFee { get; set; }
        public decimal NotificationFee { get; set; }
        public decimal InterestRatePercent { get; set; }
        public int NumberOfInterestFreeMonths { get; set; }
        public int NumberOfPaymentFreeMonths { get; set; }
        public decimal FromAmount { get; set; }
        public decimal ToAmount { get; set; }
    }
}
