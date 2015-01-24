

namespace ContentByMail.Core.EmailProcessor
{

    using System.Collections.Generic;
    using ContentByMail.Common;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using System.Linq;
    using Sitecore.Data.Items;

    internal class EmailProcessorTemplateTokenFactory
    {
        /// <summary>
        /// Creates the EmailProcessorTemplateToken.
        /// </summary>
        internal static EmailProcessorTemplateToken Create(ID itemId)
        {
            Item item = Factory.GetDatabase(Constants.Databases.Web).GetItem(itemId);

            return Create(item);

        }


        /// <summary>
        /// Creates the EmailProcessorTemplateToken.
        /// </summary>
        internal static EmailProcessorTemplateToken Create(Item item)
        {
            return new EmailProcessorTemplateToken(item);

        }


        /// <summary>
        /// Creates a collection of EmailProcessorTemplateTokens.
        /// </summary>
        internal static IEnumerable<EmailProcessorTemplateToken> CreateCollection(IEnumerable<Item> items)
        {
            return items.Select(Create);

        }
    }
}
