using System.Collections.Generic;
using System.Collections.Specialized;
using Sitecore.Data.Items;
using ThreeManCrew.ContentByMail.Common;

namespace ThreeManCrew.ContentByMail.Core.EmailProcessor
{

    internal class EmailProcessorTemplateTokenFactory
    {
        /// <summary>
        /// Creates a EmailProcessorTemplateToken.
        /// </summary>
        internal static EmailProcessorTemplateToken Create(string key, string value)
        {
            return new EmailProcessorTemplateToken(key, value);
        }

        /// <summary>
        /// Creates a collection of EmailProcessorTemplateTokens.
        /// </summary>
        internal static IEnumerable<EmailProcessorTemplateToken> CreateCollection(Item item)
        {
            NameValueCollection nameValueCollection = item.GetNameValueList(Constants.Fields.EmailProcessorTemplate.TokenToFieldList);

            foreach (string key in nameValueCollection.AllKeys)
            {
                yield return Create(key, nameValueCollection[key]);
            }
        }
    }
}
