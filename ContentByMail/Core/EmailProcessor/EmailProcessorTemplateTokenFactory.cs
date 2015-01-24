using System.Collections.Specialized;
using ContentByMail.Common;
using Sitecore.Data.Items;

namespace ContentByMail.Core.EmailProcessor
{

    using System.Collections.Generic;
    using System.Linq;

    internal class EmailProcessorTemplateTokenFactory
    {
        /// <summary>
        /// Creates a EmailProcessorTemplateToken.
        /// </summary>
        internal static EmailProcessorTemplateToken Create(KeyValuePair<string,string> keyValue )
        {
            return new EmailProcessorTemplateToken(keyValue);
        }

        /// <summary>
        /// Creates a collection of EmailProcessorTemplateTokens.
        /// </summary>
        internal static IEnumerable<EmailProcessorTemplateToken> CreateCollection(Item item)
        {

            NameValueCollection nameValueCollection = item.GetNameValueList(Constants.Fields.EmailProcessorTemplate.TokenToFieldList);

            return from KeyValuePair<string, string> key in nameValueCollection select Create(key);
        }
    }
}
