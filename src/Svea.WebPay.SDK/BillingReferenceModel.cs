namespace Svea.WebPay.SDK
{
    public class BillingReferenceModel
    {
        public int ReferenceNumber { get; set; }
        public BillingReferenceType Type { get; set; }
        public string Value { get; set; }
    }

    public enum BillingReferenceType
    {
        Undefined = 0,
        Purchase = 1,
        ContactPerson = 2,
        InvoiceReference = 3,
        CostCenter = 4,
        InvoiceRecipient = 5,
        LetterNumber = 6,
        AccountingNumber = 7
    }

}
