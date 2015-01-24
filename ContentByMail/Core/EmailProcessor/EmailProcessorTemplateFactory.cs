﻿

namespace ContentByMail.Core.EmailProcessor
{

    using System.Collections.Generic;
    using ContentByMail.Common;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using System.Linq;
    using Sitecore.Data.Items;

    internal class EmailProcessorTemplateFactory
    {
        /// <summary>
        /// Creates the EmailProcessorTemplate
        /// </summary>
        internal static EmailProcessorTemplate Create(ID itemId)
        {
            Item item = Factory.GetDatabase(Constants.Databases.Web).GetItem(itemId);

            return Create(item);

        }


        /// <summary>
        /// Creates the EmailProcessorTemplate
        /// </summary>
        internal static EmailProcessorTemplate Create(Item item)
        {
            return new EmailProcessorTemplate(item);

        }


        /// <summary>
        /// Creates a collection of EmailProcessorTemplates.
        /// </summary>
        internal static IEnumerable<EmailProcessorTemplate> CreateCollection(IEnumerable<Item> items)
        {
            return items.Select(Create);

        }
    }
}
