namespace Sample.AspNetCore.SystemTests.Test.Helpers
{
    public static class PaymentMethods
    {
        public enum Option
        {
            Card,
            Invoice,
            DirektBank,
            Trustly,
            PaymentPlan,
            AccountCredit,
            BlackFriday,
            Swish,
        }
    }
}