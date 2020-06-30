using Newtonsoft.Json.Serialization;

namespace Svea.WebPay.SDK.Json
{
    public class LowercaseContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Resolves the name of the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// Resolved name of the property.
        /// </returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
