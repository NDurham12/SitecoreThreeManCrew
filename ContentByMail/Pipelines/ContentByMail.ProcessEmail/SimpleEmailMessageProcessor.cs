using ContentByMail.Common;
using ContentByMail.Common.Enumerations;
using ContentByMail.Core.EmailProcessor;
using ContentByMail.Core.Notifications;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Security.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Constants = ContentByMail.Common.Constants;

namespace ContentByMail.Pipelines.ContentByMail.ProcessEmail
{
    using Sitecore.Diagnostics;

    public class SimpleEmailMessageProcessor : IEmailMessageProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleEmailMessageProcessor"/> class.
        /// </summary>
        public SimpleEmailMessageProcessor() { }

        /// <summary>
        /// Processes the specified message.
        /// </summary>
        /// <param name="args">The args.</param>
        public void Process(PostmarkMessageArgs args)
        {
            try
            {
                Assert.ArgumentNotNull(args, "args");
                Assert.IsNotNull(args.Message, "args.Message");

                string template = args.MessageTokenValues["Template"];

                Item contentEmailModule = Context.ContentDatabase.GetItem(Constants.Settings.ContentByEmailModuleItem);

                Item emailProcessorItem = contentEmailModule.Axes.GetDescendants().FirstOrDefault(item => item.IsDerived(Constants.Templates.EmailProcessorTemplate) 
                                                                                                         && item[Constants.Fields.EmailProcessorTemplate.EmailTokenName] == template);

                if (emailProcessorItem != null)
                {
                    Item parentFolder = Context.ContentDatabase.GetItem(emailProcessorItem[Constants.Fields.EmailProcessorTemplate.Folder]);
                    TemplateID newItemTemplateId = new TemplateID(new ID(emailProcessorItem[Constants.Fields.EmailProcessorTemplate.Template]));
                    bool createAsuser = emailProcessorItem.GetCheckBoxValue(Constants.Fields.EmailProcessorTemplate.AssociateSenderToUserProfile);
                    bool autoProcessFields = emailProcessorItem.GetCheckBoxValue(Constants.Fields.EmailProcessorTemplate.AutoProcessTokensToFields);
                
                    User account = null;

                    List<string> missingFieldFlag = new List<string>();
                    Dictionary<string, string> tokenFieldList = new Dictionary<string, string>();

                    if (parentFolder != null)
                    {
                        if (createAsuser)
                        {
                            string username = Membership.GetUserNameByEmail(args.Message.From);
                            
                            if (!String.IsNullOrEmpty(username) && User.Exists(username))
                            {
                                account = User.FromName(username, true);
                            }
                            else
                            {
                                account = Constants.Security.ServiceUser;
                            }
                        }

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

                                    newItem[messageTokenValue.Key] = messageTokenValue.Value;
                                }
                            }
                            else
                            {
                                EmailProcessorTemplate emailProcessorTemplate = EmailProcessorTemplateFactory.CreateCollection().FirstOrDefault();

                                Assert.IsNotNull(emailProcessorTemplate, "EmailProcessorTemplate is empty");

                                foreach (EmailProcessorTemplateToken token in emailProcessorTemplate.EmailTokens)
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

                    NotificationMessageFactory factory = new NotificationMessageFactory();
                    NotificationMessage notificationMessage = factory.CreateMessage(new ID(emailProcessorItem[Constants.Fields.EmailProcessorTemplate.NotificationTemplate]));
                    NotificationManager manager = new NotificationManager();
                    NotificationMessageType type = (missingFieldFlag.Count > 0) ? NotificationMessageType.InvalidField : NotificationMessageType.Success;

                    manager.Send(args.Message.From, Constants.DefaultContentModule.DefaultMessage, type);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Process", ex, this);
            }                                  
        }
    }
}
