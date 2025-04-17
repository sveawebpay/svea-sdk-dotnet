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

        public Customer(int id, string nationalId, string countryCode, bool isCompany, string vatNumber, bool isVerified)
        {
            Id = id;
            NationalId = nationalId;
            CountryCode = countryCode;
            IsCompany = isCompany;
            VatNumber = vatNumber;
            IsVerified = isVerified;
        }

        public int Id { get; }
        public string NationalId { get; }
        public string CountryCode { get; }
        public bool IsCompany { get; }
        public string VatNumber { get; }
        public bool IsVerified { get; }
    }
}