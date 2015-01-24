namespace ContentByMail.Core.EmailProcessor
{
    using ContentByMail.Common;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    internal class EmailProcessorTemplateToken
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailProcessorTemplateToken"/> class.
        /// </summary>
        internal EmailProcessorTemplateToken(Item item)
        {
            Assert.ArgumentNotNull(item, "EmailProcessorTemplate item is null");
            Assert.IsTrue(item.IsDerived(Constants.Templates.EmailProcessorTemplateToken), "Derives from wrong template");

            this.Name = item.GetString(Constants.Fields.EmailProcessorTemplateTokens.EmailProcessorTemplateToken);
        }
    }
}
