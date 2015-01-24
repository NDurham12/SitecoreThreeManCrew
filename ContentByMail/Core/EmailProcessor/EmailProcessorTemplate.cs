namespace ContentByMail.Core.EmailProcessor
{
    using ContentByMail.Common;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using System.Collections.Generic;

    public class EmailProcessorTemplate
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        internal string Id { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// Gets or sets the EmailTokens.
        /// </summary>
        internal IEnumerable<EmailProcessorTemplateToken> EmailTokens { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailProcessorTemplate"/> class.
        /// </summary>
        /// <param name="item">The template.</param>
        internal EmailProcessorTemplate(Item item)
        {
            Assert.ArgumentNotNull(item, "EmailProcessorTemplate item is null");
            Assert.IsTrue(item.IsDerived(Constants.Templates.EmailProcessorTemplate), "EmailProcessorTemplate derives from wrong template");

            this.Id = item.ID.ToString();
            this.Name = item.Name;
            this.EmailTokens = EmailProcessorTemplateTokenFactory.CreateCollection(item.GetMultiListValues(Constants.Fields.EmailProcessorTemplate.EmailProcessorTemplateTokenList));
        }
    }
}