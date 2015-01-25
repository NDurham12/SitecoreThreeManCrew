using System.Collections.Generic;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using ThreeManCrew.ContentByMail.Common;

namespace ThreeManCrew.ContentByMail.Core.EmailProcessor
{
    public class EmailProcessorTemplate
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EmailProcessorTemplate" /> class.
        /// </summary>
        /// <param name="item">The template.</param>
        internal EmailProcessorTemplate(Item item)
        {
            Assert.ArgumentNotNull(item, "EmailProcessorTemplate item is null");
            Assert.IsTrue(item.IsDerived(Constants.Templates.EmailProcessorTemplate),
                "EmailProcessorTemplate derives from wrong template");

            Id = item.ID.ToString();
            EmailTemplateName = item.GetString(Constants.Fields.EmailProcessorTemplate.EmailTokenName);
            ItemTemplateToCreateItemFrom = item.GetDropLinkSelectedItem(Constants.Fields.EmailProcessorTemplate.Template);
            FolderTemplateToInsertCreatedItemIn =
                item.GetDropLinkSelectedItem(Constants.Fields.EmailProcessorTemplate.Folder);
            AutoProcessFields = item.GetCheckBoxValue(Constants.Fields.EmailProcessorTemplate.AutoProcessTokensToFields);
            CreateAsuser = item.GetCheckBoxValue(Constants.Fields.EmailProcessorTemplate.AssociateSenderToUserProfile);
            NotificationTemplate =
                item.GetDropLinkSelectedItem(Constants.Fields.EmailProcessorTemplate.NotificationTemplate);
            EmailTokens = EmailProcessorTemplateTokenFactory.CreateCollection(item);
        }

        /// <summary>
        ///     Gets or sets the Id.
        /// </summary>
        internal string Id { get; set; }

        /// <summary>
        ///     Gets or sets the Name.
        /// </summary>
        internal string EmailTemplateName { get; set; }

        /// <summary>
        ///     Gets or sets the item template to create item from.
        /// </summary>
        internal Item ItemTemplateToCreateItemFrom { get; set; }

        /// <summary>
        ///     Gets or sets the folder template to insert created item in.
        /// </summary>
        internal Item FolderTemplateToInsertCreatedItemIn { get; set; }

        /// <summary>
        ///     Gets or sets the EmailTokens.
        /// </summary>
        internal IEnumerable<EmailProcessorTemplateToken> EmailTokens { get; set; }

        /// <summary>
        ///     Gets or sets the folder template to insert created item in.
        /// </summary>
        internal Item NotificationTemplate { get; set; }

        /// <summary>
        ///     Gets or sets the AutoProcessFields.
        /// </summary>
        internal bool AutoProcessFields { get; set; }

        /// <summary>
        ///     Gets or sets the CreateAsuser.
        /// </summary>
        internal bool CreateAsuser { get; set; }
    }
}