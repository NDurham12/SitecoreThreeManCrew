namespace ContentByMail.Core.EmailProcessor
{
<<<<<<< HEAD
    using ContentByMail.Common;
    using ContentByMail.Core.Notifications;
=======

    using ContentByMail.Common;
>>>>>>> 1add5dcc08bc83a429eaa24e19e9667b5820438b
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    internal class EmailProcessorTemplateToken
    {
        internal string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailProcessorTemplateToken"/> class.
        /// </summary>
        internal EmailProcessorTemplateToken(Item item)
        {
            Assert.ArgumentNotNull(item, "EmailProcessorTemplate item is null");
            Assert.IsTrue(item.IsDerived(Constants.Templates.EmailProcessorTemplateToken), "Derives from wrong template");

            Name = item.GetString(Constants.Fields.EmailProcessorTemplateTokens.EmailProcessorTemplateToken);
        }
    }
}
