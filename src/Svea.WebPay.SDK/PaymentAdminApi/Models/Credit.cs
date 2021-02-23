namespace Svea.WebPay.SDK.PaymentAdminApi.Models
{
    using Svea.WebPay.SDK.PaymentAdminApi.Response;

    using System.Collections.Generic;

    public class Credit
    {
        public Credit(IList<string> actions, MinorUnit amount, IList<OrderRowResponseObject> orderRows)
        {
            Actions = actions;
            Amount = amount;
            OrderRows = orderRows;
        }

        public IList<string> Actions { get; }
        public MinorUnit Amount { get; }
        public IList<OrderRowResponseObject> OrderRows { get; }
    }
}
