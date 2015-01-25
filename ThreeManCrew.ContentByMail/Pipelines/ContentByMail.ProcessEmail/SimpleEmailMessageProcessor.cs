using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;
using ThreeManCrew.ContentByMail.Common;
using ThreeManCrew.ContentByMail.Core.EmailProcessor;
using ThreeManCrew.ContentByMail.Core.Notifications;

namespace ThreeManCrew.ContentByMail.Pipelines.ContentByMail.ProcessEmail
{
    public class SimpleEmailMessageProcessor : IEmailMessageProcessor
    {
        /// <summary>
        ///     Processes the specified message.
        /// </summary>
        /// <param name="args">The args.</param>
        public void Process(PostmarkMessageArgs args)
        {
            try
            {
                Assert.ArgumentNotNull(args, "args");
                Assert.IsNotNull(args.Message, "args.Message");

                var template = args.MessageTokenValues["Template"];

                var emailProcessorTemplates = EmailProcessorTemplateFactory.CreateCollection();

                var emailProcessorTemplate =
                    emailProcessorTemplates.FirstOrDefault(
                        emailProcessor => emailProcessor.EmailTemplateName == template);

                Assert.IsNotNull(emailProcessorTemplate, String.Format("{0} processorTemplate", template));


                if (emailProcessorTemplate == null)
                    return;

                var parentFolder = emailProcessorTemplate.FolderTemplateToInsertCreatedItemIn;
                var newItemTemplateId = new TemplateID(emailProcessorTemplate.ItemTemplateToCreateItemFrom.ID);
                var createAsuser = emailProcessorTemplate.CreateAsuser;
                var autoProcessFields = emailProcessorTemplate.AutoProcessFields;


                User account = null;

                var missingFieldFlag = new List<string>();

                if (parentFolder == null)
                    return;


                if (createAsuser)
                {
                    var username = Membership.GetUserNameByEmail(args.Message.From);

                    if (!String.IsNullOrEmpty(username) && User.Exists(username))
                    {
                        account = User.FromName(username, true);
                    }
                    else
                    {
                        account = Constants.Security.ServiceUser;
                    }
                }

                CreateItems(args, account, parentFolder, newItemTemplateId, autoProcessFields, missingFieldFlag,
                    emailProcessorTemplate);

                SendNotificationMessage(args, emailProcessorTemplate, missingFieldFlag);
            }
            catch (Exception ex)
            {
                Log.Error("Process", ex, this);
            }
        }

        private void SendNotificationMessage(PostmarkMessageArgs args, EmailProcessorTemplate emailProcessorTemplate,
            List<string> missingFieldFlag)
        {
            var factory = new NotificationMessageFactory();
            var notificationMessage = factory.CreateMessage(emailProcessorTemplate.NotificationTemplate.ID);

            var manager = new NotificationManager();
            var type = (missingFieldFlag.Count > 0)
                ? NotificationMessageType.InvalidField
                : NotificationMessageType.Success;

            manager.Send(args.Message.From, notificationMessage, type);
        }

        private void CreateItems(PostmarkMessageArgs args, User account, Item parentFolder, TemplateID newItemTemplateId,
            bool autoProcessFields, List<string> missingFieldFlag, EmailProcessorTemplate emailProcessorTemplate)
        {
            using (new UserSwitcher(account))
            {
                parentFolder.Editing.BeginEdit();

                var newItem = parentFolder.Add(ItemUtil.ProposeValidItemName(args.Message.Subject), newItemTemplateId);

                if (autoProcessFields)
                {
                    foreach (var messageTokenValue in args.MessageTokenValues)
                    {
                        if (newItem.Fields[messageTokenValue.Key] == null)
                        {
                            missingFieldFlag.Add(messageTokenValue.Key);
                        }

                        newItem[messageTokenValue.Key] = messageTokenValue.Value;
                    }
                }
                else
                {
                    foreach (var token in emailProcessorTemplate.EmailTokens)
                    {
                        if (args.MessageTokenValues.ContainsKey(token.CustomField))
                        {
                            newItem[token.CustomField] = args.MessageTokenValues[token.SitecoreField];
                        }
                    }
                }

                parentFolder.Editing.EndEdit();
            }
        }
    }
}