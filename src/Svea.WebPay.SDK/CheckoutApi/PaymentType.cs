namespace Svea.WebPay.SDK.CheckoutApi
{
    public enum PaymentType
    {
        Unknown = default,
        None,

        /// <summary>
        /// Invoice order
        /// </summary>
        INVOICE,

        ADMININVOICE,

        ACCOUNT,

        SWISH,

        VIPPS,

        /// <summary>
        /// PaymentPlan order
        /// </summary>
        PAYMENTPLAN,

        SVEACARDPAY,

        SVEACARDPAY_PF,

        /// <summary>
        ///  (BankAxess, Norway)
        /// </summary>
        BANKAXESS,

        /// <summary>
        /// (Aktia, Finland)
        /// </summary>
        DBAKTIAFI,

        /// <summary>
        /// (Ålandsbanken, Finland)
        /// </summary>
        DBALANDSBANKENFI,

        /// <summary>
        /// (Danske bank, Sweden)
        /// </summary>
        DBDANSKEBANKSE,

        /// <summary>
        /// (Nordea, Finland)
        /// </summary>
        DBNORDEAFI,

        /// <summary>
        /// (Nordea, Sweden)
        /// </summary>
        DBNORDEASE,

        /// <summary>
        /// (OP-Pohjola, Finland)
        /// </summary>
        DBPOHJOLAFI,

        /// <summary>
        /// (Sampo, Finland)
        /// </summary>
        DBSAMPOFI,

        /// <summary>
        /// (SEB, Individuals, Sweden)
        /// </summary>
        DBSEBSE,

        /// <summary>
        /// (SEB, companies, Sweden)
        /// </summary>
        DBSEBFTGSE,

        /// <summary>
        /// (Handelsbanken, Sweden)
        /// </summary>
        DBSHBSE,

        /// <summary>
        /// (S-Pankki, Finland)
        /// </summary>
        DBSPANKKIFI,

        /// <summary>
        /// (Swedbank, Sweden)
        /// </summary>
        DBSWEDBANKSE,

        /// <summary>
        ///  (Tapiola, Finland)
        /// </summary>
        DBTAPIOLAFI,

        /// <summary>
        /// Trustly
        /// </summary>
        TRUSTLY,

        LEASINGMANUAL,

        MOBILEPAY
    }
}
