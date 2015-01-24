

namespace ContentByMail.Core.EmailProcessor
{
    using ContentByMail.Common;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using System.Linq;

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

            Item emailProcessorTemplatesFolderTemplate = DatabaseService.ActiveDatabase.GetItem(Constants.Templates.EmailProcessorTemplatesFolder);

            Assert.IsNotNull(emailProcessorTemplatesFolderTemplate, "EmailProcessorTemplatesFolderTemplate is missing");

            Item emailProcessorTemplatesFolderItem = emailProcessorTemplatesFolderTemplate.GetReferrersAsItems().FirstOrDefault();

            Assert.IsNotNull(emailProcessorTemplatesFolderItem, "EmailProcessorTemplatesFolder is missing");

            return emailProcessorTemplatesFolderItem.GetChildren().Select(Create);
        }
    }
}
