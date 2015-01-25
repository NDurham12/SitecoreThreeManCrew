namespace ContentByMail.Common
{
    using ContentByMail.Core.Notifications;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Security.Accounts;

    internal class Constants
    {
        internal static class Databases
        {
            internal const string Master = "master";
            internal const string Web = "web";
        }

        internal static class Security
        {
            private static User _serviceaccount;

            public static User ServiceUser
            {
                get { return _serviceaccount ?? (_serviceaccount = User.Exists(Settings.Account) ? User.FromName(Settings.Account, true) : null); }
            }
        }

        internal static class Settings
        {
            internal static ID EmailContentProcessorTemplatesFolder = new ID(Sitecore.Configuration.Settings.GetSetting("ContentByEmail.EmailContentProcessorTemplatesFolder"));
            internal static ID ContentByEmailModuleItem = new ID(Sitecore.Configuration.Settings.GetSetting("ContentByEmail.ModuleItem"));
            internal static ID EmailRequestHistoryItemId = new ID(Sitecore.Configuration.Settings.GetSetting("ContentByEmail.EmailRequestHistory"));
            internal static string Account = Sitecore.Configuration.Settings.GetSetting("ContentByEmail.ServiceAccount");
            internal static string TokenStartEndMultilineRegex = Sitecore.Configuration.Settings.GetSetting("ContentByEmail.TokenStartEndMultilineRegex");
            internal static string TokenTextInside = Sitecore.Configuration.Settings.GetSetting("ContentByEmail.TokenTextInside");
            internal static string TokenMissingEnding = Sitecore.Configuration.Settings.GetSetting("ContentByEmail.TokenMissingEnding");
            internal static bool ContentByEmailEmailEnableSsl = Sitecore.Configuration.Settings.GetBoolSetting("ContentByEmail.EmailEnableSsl", false);
        }

        internal static class Fields
        {
            internal static class Notification
            {
                internal static ID Sender = new ID("{365BF877-94F8-49AB-BD20-2C64208A5911}");

                internal static ID SuccessBody = new ID("{B68F12EE-DAA8-48C4-8F87-3CB5BF153823}");
                internal static ID SucessSubject = new ID("{7257D4F6-F68D-4309-A136-DD9292B350F5}");

                internal static ID InvalidTemplateSubject = new ID("{9E103554-3DC8-495D-8E89-168B7A3DF961}");
                internal static ID InvalidTemplateBody = new ID("{34E10C78-13A4-4CBF-B181-FA6A74F1BF9F}");

                internal static ID InvalidFieldTokenSubject = new ID("{21B74116-4E24-4A93-8F21-C8A18C9B2FAB}");
                internal static ID InvalidFieldTokenBody = new ID("{84512DAF-B414-4C90-8E34-918EC2A0CD1E}");

                internal static ID GenericFailureNotificationSubject = new ID("{198985EC-64BD-44AE-A080-15236D2C7E2D}");
                internal static ID GenericFailureNotificationBody = new ID("{F6CE542B-BA9B-48B6-9C2A-F6DEB521A3AD}");
            }

            internal static class MailManager
            {
                internal static ID FallbackNotificationAddress = new ID("{8C4D5944-8FA8-4AC8-93E5-791F2DCEC132}");
                internal static ID DefaultNotificationTemplate = new ID("{37B94B83-2AB0-45E1-A99E-B83128A2EB76}");
            }

            internal static class EmailRequestHistory
            {
                internal static ID Message = new ID("{59D5E2AA-1A81-4890-82E4-C0AB0097E896}");
            }

            internal static class EmailProcessorTemplate
            {
                internal static ID EmailTokenName = new ID("{A3C72B7E-92AB-42E7-AF75-7FE630497DA6}");
                internal static ID Template = new ID("{65D4DDDE-38D1-45B3-8252-EACC56D0BF03}");
                internal static ID Folder = new ID("{F35256FC-D021-43B7-9EA0-78C356AA0565}");
                internal static ID NotificationTemplate = new ID("{426225D4-977C-46FC-B862-F1ADAACA8DBD}");
                internal static ID AssociateSenderToUserProfile = new ID("{0E8465B1-9ED7-4E63-A67D-6468BA530021}");
                internal static ID AutoProcessTokensToFields = new ID("{DA092733-DDEE-4E57-A171-C47FB4106935}");
                internal static ID TokenToFieldList = new ID("{B60B5BD5-5FDC-4497-B102-541FC9403879}");
            }
        }

        internal static class Templates
        {
            internal static ID EmailProcessorTemplate = new ID("{234AD4AA-AE3A-4607-93A8-E0021E3BE107}");
            internal static ID EmailContentRequestHistory = new ID("{F2FB43E3-2B51-40B8-BE64-1EF3CB2EEA0D}");
        }

        public static class DefaultContentModule
        {
            public static string FallBackAddress;
            public static NotificationMessage DefaultMessage;

            static DefaultContentModule()
            {
                Item mainContentModule = Factory.GetDatabase(Constants.Databases.Web).GetItem(Constants.Settings.ContentByEmailModuleItem);

                if (mainContentModule != null)
                {
                    FallBackAddress = mainContentModule[Constants.Fields.MailManager.FallbackNotificationAddress];

                    ID defaultNotificationTemplateId = new ID(mainContentModule[Constants.Fields.MailManager.DefaultNotificationTemplate]);           
                    NotificationMessageFactory factory = new NotificationMessageFactory();

                    DefaultMessage = factory.CreateMessage(defaultNotificationTemplateId);
                }
            }
        }
    }
}
