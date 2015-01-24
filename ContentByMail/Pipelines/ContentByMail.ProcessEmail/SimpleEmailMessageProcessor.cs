using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using ContentByMail.Common.Enumerations;
using ContentByMail.Core.Notifications;
using ContentByMail.Common;
using ContentByMail.Core.EmailProcessor;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Security.Accounts;
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


                var template = args.MessageTokenValues["Template"];

                var contentEmailModule = Context.ContentDatabase.GetItem(Constants.Settings.ContentByEmailModuleItem);

                var emailProcessorItem = contentEmailModule.Axes.GetDescendants().FirstOrDefault(item => item.IsDerived(Constants.Templates.EmailProcessorTemplate) 
                                                                                                         && item[Constants.Fields.EmailProcessorTemplate.EmailTokenName] == template);

                if (emailProcessorItem != null)
                {
                    var parentFolder = Context.ContentDatabase.GetItem(emailProcessorItem[Constants.Fields.EmailProcessorTemplate.Folder]);
                    var newItemTemplateId = new TemplateID(new ID(emailProcessorItem[Constants.Fields.EmailProcessorTemplate.Template]));
                    var createAsuser = emailProcessorItem.GetCheckBoxValue(Constants.Fields.EmailProcessorTemplate.AssociateSenderToUserProfile);
                    var autoProcessFields = emailProcessorItem.GetCheckBoxValue(Constants.Fields.EmailProcessorTemplate.AutoProcessTokensToFields);
                
                    User account = null;

                    var missingFieldFlag = new List<string>();
                    var tokenFieldList = new Dictionary<string, string>();

                    if (parentFolder != null)
                    {
                    
                        if (createAsuser)
                        {
                            var username = Membership.GetUserNameByEmail(args.Message.From);
                            if (!string.IsNullOrEmpty(username) && User.Exists(username))
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
                                EmailProcessorTemplate emailProcessorTemplate = EmailProcessorTemplateFactory.CreateCollection().FirstOrDefault();

                                Assert.IsNotNull(emailProcessorTemplate, "EmailProcessorTemplate is empty");

                                foreach (EmailProcessorTemplateToken token in emailProcessorTemplate.EmailTokens)
                                {
                                    if (args.MessageTokenValues.ContainsKey(token.CustomField))
                                        newItem[token.CustomField] = args.MessageTokenValues[token.SitecoreField];
                                }
                            }                        

                            parentFolder.Editing.EndEdit();
                        }                    
                    }

                    var factory = new NotificationMessageFactory();
                    var notificationMessage = factory.CreateMessage(new ID(emailProcessorItem[Constants.Fields.EmailProcessorTemplate.NotificationTemplate]));
                    NotificationManager manager = new NotificationManager();
                    NotificationMessageType type;

                    if (missingFieldFlag.Count > 0)
                    {
                        type = NotificationMessageType.InvalidField;
                    }
                    else
                    {
                        type = NotificationMessageType.Success;

                    }

                    manager.Send(args.Message.From, Constants.DefaultContentModule.DefaultMessage, type);

                    Log.Info("In SimpleEmailMessageProcessor", this);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Process", ex, this);
            }                                  
        }
    }
}
