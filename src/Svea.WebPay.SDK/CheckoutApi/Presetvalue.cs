namespace Svea.WebPay.SDK.CheckoutApi
{
    public class Presetvalue
    {
        /// <summary>
        /// List of presetvalue typenames
        /// NationalId (String) Company specific validation.
        /// EmailAddress(String) Max 50 characters.Will be validated as an email address.
        /// PhoneNumber(String) 1-18 digits, can include “+”, “-“s and space.
        /// PostalCode(String) Company specific validation.
        /// IsCompany(Boolean) Required if nationalid is set.
        /// </summary>
        /// <param name="typeName">
        /// <summary>
        /// Name of the field you want to set
        /// </summary>
        /// </param>
        /// <param name="value"></param>
        /// <param name="isReadonly">
        /// <summary>
        /// Should the preset value be locked for editing, set readonly to true.
        /// Usable if you only let your registered users use the checkout.
        /// </summary>
        /// </param>
        public Presetvalue(string typeName, string value, bool isReadonly)
        {
            TypeName = typeName;
            Value = value;
            IsReadonly = isReadonly;
        }

        /// <summary>
        /// Name of the field you want to set
        /// </summary>
        public string TypeName { get; }

        public string Value { get; }

        /// <summary>
        /// Should the preset value be locked for editing, set readonly to true.
        /// Usable if you only let your registered users use the checkout.
        /// </summary>
        public bool IsReadonly { get; }
    }
}