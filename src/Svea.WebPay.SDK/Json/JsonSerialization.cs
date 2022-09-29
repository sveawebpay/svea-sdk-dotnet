namespace Svea.WebPay.SDK.Json
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public static class JsonSerialization
    {
        static JsonSerialization()
        {
            Settings = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                //IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                MaxDepth = 64,
                IgnoreReadOnlyProperties = false,
                Converters =
                {
                    new CustomEmailAddressConverter(),
                    new CustomCurrencyCodeConverter(),
                    new CustomLanguageConverter(),
                    new CustomMinorUnitConverter(),
                    new CustomRegionInfoConverter(),
                    new CustomUriConverter(),
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
        }

        public static JsonSerializerOptions Settings { get; }
    }
}