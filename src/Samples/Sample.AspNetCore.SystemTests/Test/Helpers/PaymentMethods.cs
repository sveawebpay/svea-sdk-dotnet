namespace Sample.AspNetCore.SystemTests.Test.Helpers
{
    public static class PaymentMethods
    {
        public enum Option
        {
            Card,
            CardEmbedded,
            Invoice,
            DirektBank,
            Trustly,
            PaymentPlan,
            AccountCredit,
            BlackFriday,
            Swish,
            Vipps,
            Leasing,
            MobilePay,
        }
    }
}