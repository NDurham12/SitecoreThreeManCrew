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
            internal const ID NotificationOnFailureItemId = new ID(Sitecore.Configuration.Settings.GetSetting("Notification.OnFailure"));
            internal const ID NotificationOnSuccessItemId = new ID(Sitecore.Configuration.Settings.GetSetting("Notification.OnSuccess"));
        }

        internal static class Fields
        {
            internal static class Notification
            {
                internal const ID Sender = new ID("");
                internal const ID Subject = new ID("");
                internal const ID Body = new ID("");
            }
        }
    }
}
