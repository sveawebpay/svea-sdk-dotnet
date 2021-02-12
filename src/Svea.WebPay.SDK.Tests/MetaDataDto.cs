using System.Collections.Generic;

namespace Svea.WebPay.SDK.Tests
{
    public class MetaDataDto
    {
        internal class MetadataDto : Dictionary<string, object>
        {
            public MetadataDto()
            {
            }

            public MetadataDto(IDictionary<string, object> dictionary) : base(dictionary)
            {
            }

        }
    }
}
