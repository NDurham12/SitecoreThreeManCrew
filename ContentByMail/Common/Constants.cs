namespace ContentByMail.Common
{
    using Sitecore.Configuration;
    using Sitecore.Data;

    internal class Constants
    {
        internal static class Databases
        {
            internal const string Core = "core";
            internal const string Master = "master";
            internal const string Web = "web";
        }

        internal static class Settings
        {
            internal static ID NotificationOnFailureItemId = new ID(Sitecore.Configuration.Settings.GetSetting("Notification.OnFailure"));
            internal static ID NotificationOnSuccessItemId = new ID(Sitecore.Configuration.Settings.GetSetting("Notification.OnSuccess"));
        }

        internal static class Fields
        {
            internal static class Notification
            {
                internal static ID Sender = new ID("");
                internal static ID Subject = new ID("");
                internal static ID Body = new ID("");
            }

            internal static class EmailProcessorTemplate
            {
                internal static ID Name = new ID("");
                internal static ID EmailProcessorTemplateTokenList = new ID("");
            }

            internal static class EmailProcessorTemplateTokens
            {
                internal static ID EmailProcessorTemplateToken = new ID("");
            }


        }

        internal static class Templates
        {
            internal static ID EmailProcessorTemplate = new ID("{234AD4AA-AE3A-4607-93A8-E0021E3BE107}");

            internal static ID EmailProcessorTemplateToken = new ID("{234AD4AA-AE3A-4607-93A8-E0021E3BE107}");
        }
    }
}
