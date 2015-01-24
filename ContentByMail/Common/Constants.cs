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
                internal const ID Sender = new ID("{365BF877-94F8-49AB-BD20-2C64208A5911}");
                internal const ID Subject = new ID("{198985EC-64BD-44AE-A080-15236D2C7E2D}");
                internal const ID Body = new ID("{F6CE542B-BA9B-48B6-9C2A-F6DEB521A3AD}");
            }
        }
    }
}
