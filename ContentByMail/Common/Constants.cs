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
            
            
            internal static class EmailProcessorTemplate
            {
                internal static ID Name = new ID("");
                internal static ID EmailProcessorTemplateTokenList = new ID("");
            }

            internal static class EmailProcessorTemplateTokens
            {
                internal static ID EmailProcessorTemplateToken = new ID("");
                internal static ID FallbackNotificationAddress = new ID("{8C4D5944-8FA8-4AC8-93E5-791F2DCEC132}");
            }

            internal static class EmailLog
            {
                internal static ID Message = new ID("{59D5E2AA-1A81-4890-82E4-C0AB0097E896}");
                internal static ID Notes = new ID("{98F3999C-877E-409B-9FF7-925C0DA783C9}");
            }

            internal static class EmailProcessorSettings
            {
                internal static ID EmailTokenName = new ID("{A3C72B7E-92AB-42E7-AF75-7FE630497DA6}");
                internal static ID Template = new ID("{65D4DDDE-38D1-45B3-8252-EACC56D0BF03}");
                internal static ID NotificationTemplate = new ID("{426225D4-977C-46FC-B862-F1ADAACA8DBD}");
                internal static ID EmailProcessorType = new ID("{BEAADB4B-4581-466C-9CE0-3B8467DBE9C9}");
                internal static ID AssociateSenderToUserProfile = new ID("{0E8465B1-9ED7-4E63-A67D-6468BA530021}");
                internal static ID AttachmentsImportFolder = new ID("{1C41D4DB-63E7-456C-BEC8-256F59FF05A0}");
                internal static ID SaveEmailsToBucket = new ID("{DD5CE860-0037-4DD3-8C9F-8174A1FD3054}");
                internal static ID AutoProcessTokensToFields = new ID("{DA092733-DDEE-4E57-A171-C47FB4106935}");
                internal static ID TokenToFieldList = new ID("{B60B5BD5-5FDC-4497-B102-541FC9403879}");             
            }


        }

        internal static class Templates
        {
            internal static ID EmailProcessorTemplate = new ID("{234AD4AA-AE3A-4607-93A8-E0021E3BE107}");

            internal static ID EmailProcessorTemplateToken = new ID("{234AD4AA-AE3A-4607-93A8-E0021E3BE107}");
        }
    }
}
