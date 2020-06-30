namespace Svea.WebPay.SDK.PaymentAdminApi.Models
{
    public class NewRow
    {
        public NewRow(long rowId, NewOrderRow row)
        {
            RowId = rowId;
            Row = row;
        }

        public long RowId { get; }
        public NewOrderRow Row { get; }
    }
}
