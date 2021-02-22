using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Svea.WebPay.SDK.Json
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    //Original code found here: https://gist.github.com/gubenkoved/999eb73e227b7063a67a50401578c3a7
    public class TypesafeEnumConverter : JsonConverter<Type>
    {
        [ThreadStatic]
        private Dictionary<Type, Dictionary<string, object>> _fromValueMap; // string representation to Enum value map

        [ThreadStatic]
        private Dictionary<Type, Dictionary<object, string>> _toValueMap; // Enum value to string map

        public string UnknownValue { get; set; } = "Unknown";

        public override Type Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            InitMap(typeToConvert);
            var underlyingType = Nullable.GetUnderlyingType(typeToConvert);
            typeToConvert = underlyingType != null ? underlyingType : typeToConvert;

            if (reader.TokenType == JsonTokenType.String)
            {
                var enumText = reader.GetString();

                var val = FromValue(typeToConvert, enumText);

                if (val != null)
                    return val;
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                var enumVal = reader.GetInt32();
                var values = (int[])Enum.GetValues(typeToConvert);
                if (values.Contains(enumVal))
                {
                    return Enum.Parse(typeToConvert, enumVal.ToString()).GetType();
                }
            }

            var names = Enum.GetNames(typeToConvert);

            var unknownName = names
                .Where(n => string.Equals(n, UnknownValue, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            if (unknownName == null)
            {
                throw new JsonException($"Unable to parse '{reader.GetString()}' to enum {typeToConvert}. Consider adding Unknown as fail-back value.");
            }

            return Enum.Parse(typeToConvert, unknownName).GetType();
        }

        public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options)
        {
            Type enumType = value.GetType();

            InitMap(enumType);

            var val = ToValue(enumType, value);

            writer.WriteStringValue(val);
        }

        private void InitMap(Type enumType)
        {
            var underlyingType = Nullable.GetUnderlyingType(enumType);
            enumType = underlyingType != null ? underlyingType : enumType;

            if (this._fromValueMap == null)
                this._fromValueMap = new Dictionary<Type, Dictionary<string, object>>();

            if (this._toValueMap == null)
                this._toValueMap = new Dictionary<Type, Dictionary<object, string>>();

            if (this._fromValueMap.ContainsKey(enumType))
                return; // already initialized

            var fromMap = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
            var toMap = new Dictionary<object, string>();

            var fields = enumType.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo field in fields)
            {
                var name = field.Name;
                var enumValue = Enum.Parse(enumType, name);

                toMap[enumValue] = name;
                fromMap[name] = enumValue;
            }

            this._fromValueMap[enumType] = fromMap;
            this._toValueMap[enumType] = toMap;
        }

        private string ToValue(Type enumType, object obj)
        {
            Dictionary<object, string> map = this._toValueMap[enumType];

            return map[obj];
        }

        private Type FromValue(Type enumType, string value)
        {
            Dictionary<string, object> map = this._fromValueMap[enumType];

            if (!map.ContainsKey(value))
                return null;

            return map[value].GetType();
        }
    }
}