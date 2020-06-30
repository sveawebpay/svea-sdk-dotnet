namespace Svea.WebPay.SDK.CheckoutApi
{
    public class Customer
    {
        public Customer(int id, string nationalId, string countryCode, bool isCompany)
        {
            Id = id;
            NationalId = nationalId;
            CountryCode = countryCode;
            IsCompany = isCompany;
        }

        public int Id { get; }
        public string NationalId { get; }
        public string CountryCode { get; }
        public bool IsCompany { get; }
    }
}