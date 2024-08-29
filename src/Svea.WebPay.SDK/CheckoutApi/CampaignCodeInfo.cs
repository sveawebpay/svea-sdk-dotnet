namespace Svea.WebPay.SDK.CheckoutApi
{
    public class CampaignCodeInfo
    {
        public CampaignCodeInfo() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="campaignCode"></param>
        /// <param name="description"></param>
        /// <param name="paymentPlanType"></param>
        /// <param name="contractLengthInMonths"></param>
        /// <param name="monthlyAnnuityFactor"></param>
        /// <param name="initialFee"></param>
        /// <param name="notificationFee"></param>
        /// <param name="interestRatePercent"></param>
        /// <param name="numberOfInterestFreeMonths"></param>
        /// <param name="numberOfPaymentFreeMonths"></param>
        /// <param name="fromAmount"></param>
        /// <param name="amount"></param>
        /// <param name="monthlyAmount">Is only calculated when an amount was added to the request.</param>
        public CampaignCodeInfo(long campaignCode, string description, PaymentPlanTypeCode paymentPlanType, int contractLengthInMonths, decimal monthlyAnnuityFactor,
            decimal initialFee, decimal notificationFee, decimal interestRatePercent, int numberOfInterestFreeMonths,
            int numberOfPaymentFreeMonths, decimal fromAmount, decimal amount, decimal? monthlyAmount = null)
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
            MonthlyAmount = monthlyAmount;
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
        /// <summary>
        /// Is only calculated when an amount was added to the request.
        /// </summary>
        public decimal? MonthlyAmount { get; set; }
    }
}
