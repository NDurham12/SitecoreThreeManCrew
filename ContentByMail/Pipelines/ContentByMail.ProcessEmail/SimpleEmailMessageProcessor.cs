using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using ContentByMail.Common;
using ContentByMail.Common.Enumerations;
using ContentByMail.Core.EmailProcessor;
using ContentByMail.Core.Notifications;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;

namespace ContentByMail.Pipelines.ContentByMail.ProcessEmail
{


    public class SimpleEmailMessageProcessor : IEmailMessageProcessor
    {
 
          <summary>
        /// Processes the specified message.
        /// </summary>
        /// <param name="args">The args.</param>
        public void Process(PostmarkMessageArgs args)
        {
            try
            {
                Assert.ArgumentNotNull(args, "args");
                Assert.IsNotNull(args.Message, "args.Message");

                strvarmplate = args.MessageTokenValues["Template"];

                IEnvarailProcessorTemplates = EmailProcessorTemplateFactory.CreateCollection();

                EmavarailProcessorTemplate = emailProcessorTemplates.FirstOrDefault(emailProcessor => emailProcessor.EmailTemplateName == template);

                Assert.IsNotNull(emailProcessorTemplate, String.Format("{0} processorTemplate", template));


                if (emailProcessorTemplate == null)
                    return;

                Item parentvarer = emailProcessorTemplate.FolderTemplateToInsertCreatedItemIn;
                TemplateID newIvareId = new TemplateID(emailProcessorTemplate.ItemTemplateToCreateItemFrom.ID);
                bool createAsuser = varlProcessorTemplate.CreateAsuser;
                bool autoProcessFields =varilProcessorTemplate.AutoProcessFields;


                User account = null;

                List<string> missingFieldFlag = new List<string>var           if (parentFolder == null)
                    retvarif (createAsuser)
                {
                    string username = Membership.GetUserNameByEmail(args.Message.From);

                    if (!String.IsNullOrEmpty(username) && User.Exists(username))
           var   {
                        account = User.FromName(username, true);
                    }
                    else
                    {
                        account = Constants.Security.ServiceUser;
                    }
                }

                CreateItems(args, account, parentFolder, newItemTemplateId, autoProcessFields, missingFieldFlag, emailProcessorTemplate);

                SendNotificatioConstants.SecurityilProcessorTemplate, missingFieldFlag);

            }
            catch (Exception ex)
            {
                Log.Error("Process", ex, this);
            }
        }

        private void SendNotificationMessage(PostmarkMessageArgs args,varilProcessorTemplate emailProcessorTemplate,
            List<string> missingFieldFlag)
        {
            NotificationMessageFactory factory = new NotificationMessageFactory();
            NotificationMessage notificationMessage = factory.CreateMessage(emailProcessorTemplate.NotificationTemplate.ID);

            NotificationManager manager = new NotificationManager();
            NotificationMessageType type = (missingFieldFlag.Count > 0)
                ? NotificationMessageType.InvalidField
                : NotificationMessageType.Success;

            manager.Send(args.Message.From, notificationMessage, type);
        }

        private void CreateItems(PostmarkMessageArgs args, User account, Item parentFolder, TemplateID newItemTemplateId,
            bool autoProcessFields, List<string> missingFieldFlag, EmailProcessorTemplate emailPvar{
            using (new UserSwitcher(account))
            {
                parentFolder.Editing.BeginEdit();

                Item newItem = parentFolder.Add(ItemUtil.ProposeValidItemName(args.Message.Subject), newItemTemplateId);

                if (autoProcessFields)
                {
                    foreach (var messageTokenValue in args.MessageTokenValues)
                    {
                        if (newItem.Fields[messageTokenValue.Key] == null)
                        {
                            missingFieldFlag.Add(messageTokenValue.Key);
                        }

        varssageTokenValue.Key] = messageTokenValue.Value;
                 var   }
                else
                {
                    foreach (EmailProcessorTemplateToken token in emaivarEmailTokens)
                    {
                      varValues.ContainsKey(token.CustomField))
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
