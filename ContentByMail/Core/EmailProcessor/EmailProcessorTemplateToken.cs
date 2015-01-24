namespace ContentByMail.Core.EmailProcessor
{
    using ContentByMail.Common;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    internal class EmailProcessorTemplateToken
    {
        internal string CustomField { get; set; }

        internal string SitecoreField { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailProcessorTemplateToken"/> class.
        /// </summary>
        internal EmailProcessorTemplateToken(Item item)
        {
            Assert.ArgumentNotNull(item, "EmailProcessorTemplate item is null");
            Assert.IsTrue(item.IsDerived(Constants.Templates.EmailProcessorTemplateToken), "Derives from wrong template");

            CustomField = item.GetString(Constants.Fields.EmailProcessorTemplateTokens.EmailProcessorTemplateTokenKeyName);

            SitecoreField = item.GetString(Constants.Fields.EmailProcessorTemplateTokens.EmailProcessorTemplateTokenTokenKeyField);
        }
    }
}
