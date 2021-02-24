namespace Svea.WebPay.SDK.PaymentAdminApi.Models
{
    using Svea.WebPay.SDK.PaymentAdminApi.Response;

    using System.Collections.Generic;

    public class Credit
    {
        /// <summary>
        /// </summary>
        /// <param name="actions">A list of possible actions on the credit.</param>
        /// <param name="amount">Credited amount. Minor currency.</param>
        /// <param name="orderRows">List of order rows.</param>
        public Credit(IList<string> actions, MinorUnit amount, IList<OrderRowResponseObject> orderRows)
        {
            Actions = actions;
            Amount = amount;
            OrderRows = orderRows;
        }

        /// <summary>
        /// A list of possible actions on the credit.
        /// </summary>
        public IList<string> Actions { get; }

        /// <summary>
        /// Credited amount. Minor currency.
        /// </summary>
        public MinorUnit Amount { get; }

        /// <summary>
        /// List of order rows.
        /// </summary>
        public IList<OrderRowResponseObject> OrderRows { get; }
    }
}
