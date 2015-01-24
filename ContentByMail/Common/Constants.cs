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

        internal static class Items
        {
            internal static ID ContentMailManager = new ID("{1DF26677-47FF-483E-B225-0A9D8BBB5112}");
        }

        internal static class Fields
        {
            internal static class Notification
            {
                internal static ID Sender = new ID("{365BF877-94F8-49AB-BD20-2C64208A5911}");
                internal static ID Subject = new ID("{198985EC-64BD-44AE-A080-15236D2C7E2D}");
                internal static ID Body = new ID("{F6CE542B-BA9B-48B6-9C2A-F6DEB521A3AD}");
            }

            internal static class MailManager
            {
                internal static ID FallbackNotificationAddress = new ID("{8C4D5944-8FA8-4AC8-93E5-791F2DCEC132}");
            }
        }
    }
}
