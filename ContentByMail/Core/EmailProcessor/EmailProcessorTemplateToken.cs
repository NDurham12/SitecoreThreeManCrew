
namespace ContentByMail.Core.EmailProcessor
{

    internal class EmailProcessorTemplateToken
    {
        internal string CustomField { get; set; }

        internal string SitecoreField { get; set; }

        internal EmailProcessorTemplateToken(string key, string value)
        {
            CustomField = key;

            SitecoreField = value;
        }
    }
}
