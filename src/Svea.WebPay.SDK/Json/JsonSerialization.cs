using Newtonsoft.Json;

using Svea.WebPay.SDK.CheckoutApi;

using System.Collections.Generic;

namespace Svea.WebPay.SDK.Json
{
    public static class JsonSerialization
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new LowercaseContractResolver(),
            StringEscapeHandling = StringEscapeHandling.EscapeNonAscii,
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new List<JsonConverter>
            {
                new TypesafeEnumConverter(),
                new CustomMinorUnitConverter(typeof(MinorUnit)),
                new CustomEmailAddressConverter(typeof(EmailAddress)),
                new CustomCurrencyCodeConverter(typeof(CurrencyCode)),
                new CustomLanguageConverter(),
                new CustomRegionInfoConverter(),
                new CustomUriConverter()
            }
        };
    }
}