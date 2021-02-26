namespace Svea.WebPay.SDK
{
    public class Address
    {
        public Address(string fullName, string firstName, string lastName, string streetAddress, string streetAddress2, string streetAddress3, string coAddress,
            string postalCode, string city, string countryCode, bool isGeneric, object addressLines)
        {
            FullName = fullName;
            FirstName = firstName;
            LastName = lastName;
            StreetAddress = streetAddress;
            StreetAddress2 = streetAddress2;
            StreetAddress3 = streetAddress3;
            CoAddress = coAddress;
            PostalCode = postalCode;
            City = city;
            CountryCode = countryCode;
            IsGeneric = isGeneric;
            AddressLines = addressLines;
        }

        public string FullName { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string StreetAddress { get; }
        public string StreetAddress2 { get; }
        public string StreetAddress3 { get; }
        public string CoAddress { get; }
        public string PostalCode { get; }
        public string City { get; }
        public string CountryCode { get; }

        /// <summary>
        /// Returns true if the address is a generic/international address i.e. one where we don't what fields there might be. Generic addresses only have values in fullName and addressLines.
        /// </summary>
        public bool IsGeneric { get; }

        /// <summary>
        /// This is only populated if the address is a generic/international address (IsGeneric returns true).
        /// </summary>
        public object AddressLines { get; }
    }
}