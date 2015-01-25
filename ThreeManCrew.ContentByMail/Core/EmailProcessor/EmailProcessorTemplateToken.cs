namespace ThreeManCrew.ContentByMail.Core.EmailProcessor
{
    internal class EmailProcessorTemplateToken
    {
        /// <summary>
        /// Gets or sets the custom field.
        /// </summary>
        /// <value>
        /// The custom field.
        /// </value>
        internal string CustomField { get; set; }

        /// <summary>
        /// Gets or sets the sitecore field.
        /// </summary>
        internal string SitecoreField { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailProcessorTemplateToken"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        internal EmailProcessorTemplateToken(string key, string value)
        {
            this.CustomField = key;
            this.SitecoreField = value;
        }
    }
}
