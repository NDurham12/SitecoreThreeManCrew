using System.Collections.Generic;
using System.Linq;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using ThreeManCrew.ContentByMail.Common;

namespace ThreeManCrew.ContentByMail.Core.EmailProcessor
{


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

        /// <summary>
        /// Creates a collection of EmailProcessorTemplates.
        /// </summary>
        internal static IEnumerable<EmailProcessorTemplate> CreateCollection()
        {
            Item emailProcessorTemplatesFolderItem = Factory.GetDatabase(Constants.Databases.Web).GetItem(Constants.Settings.EmailContentProcessorTemplatesFolder);

            Assert.IsNotNull(emailProcessorTemplatesFolderItem, "EmailProcessorTemplatesFolder is missing");

            return emailProcessorTemplatesFolderItem.GetChildren().Select(Create);
        }
    }
}
