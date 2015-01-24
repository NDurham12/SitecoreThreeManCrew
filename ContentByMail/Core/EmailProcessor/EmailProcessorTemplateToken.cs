using System.Collections.Generic;

namespace ContentByMail.Core.EmailProcessor
{
    using ContentByMail.Common;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    internal class EmailProcessorTemplateToken
    {
        internal string CustomField { get; set; }

        internal string SitecoreField { get; set; }

        internal EmailProcessorTemplateToken(KeyValuePair<string, string> keyValue)
        {
            Assert.ArgumentNotNull(keyValue, "EmailProcessorTemplateToken is null");

            CustomField = keyValue.Key;

            SitecoreField = keyValue.Value;
        }
    }
}
