namespace Svea.WebPay.SDK.PaymentAdminApi
{
    public enum FeeType
    {
        /// <summary>
        /// Administrative fee.
        /// </summary>
        Admin,

        /// <summary>
        /// Credit fee.
        /// </summary>
        Credit,

        /// <summary>
        /// Reminder fee.
        /// </summary>
        Reminder,

        /// <summary>
        /// Debt collection fee.
        /// </summary>
        Collection,

        /// <summary>
        /// Interest, that has been transferred to you. This amount could be both positive and negative.
        /// </summary>
        Interest,

        /// <summary>
        /// Postage fee.
        /// </summary>
        Postage,

        /// <summary>
        /// The amount is still correct, but we cannot categorize this certain transaction.
        /// </summary>
        Deviation,

        /// <summary>
        /// Fee moving date.
        /// </summary>
        DueDate,

        /// <summary>
        /// Fee for legal action.
        /// </summary>
        LegalAction,

        /// <summary>
        /// This can be both a paid out amount or credited kickback.
        /// </summary>
        Kickback,

        /// <summary>
        /// Deposits to limit account will be identified within this FeeType.
        /// </summary>
        Deposit,

        /// <summary>
        /// E.g. monthly fees, yearly fees, invoice layout etc.
        /// </summary>
        Service,

        /// <summary>
        /// An invoice transferred back to you.
        /// </summary>
        Dismissed,

        /// <summary>
        /// This occurs when Svea adjusts an inaccurate transaction or similar.
        /// </summary>
        Correction
    }
}


