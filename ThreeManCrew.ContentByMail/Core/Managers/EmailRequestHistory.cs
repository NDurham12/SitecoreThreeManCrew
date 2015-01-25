using System;
using PostmarkDotNet;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;
using ThreeManCrew.ContentByMail.Common;

namespace ThreeManCrew.ContentByMail.Core.Managers
{
    public class EmailRequestHistory
    {
        /// <summary>
        ///     Adds a new message to the Email Request history item bucket
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fullMessage"></param>
        public static void Add(PostmarkInboundMessage message)
        {
            try
            {
                var masterDb = Factory.GetDatabase(Constants.Databases.Master);

                if (masterDb != null)
                {
                    var historyBucket = masterDb.GetItem(Constants.Settings.EmailRequestHistoryItemId);
                    TemplateItem emailHistoryTemplate = masterDb.GetItem(Constants.Templates.EmailContentRequestHistory);

                    if (historyBucket != null && emailHistoryTemplate != null)
                    {
                        using (new UserSwitcher(Constants.Security.ServiceUser))
                        {
                            historyBucket.Editing.BeginEdit();

                            var newItem = historyBucket.Add(ItemUtil.ProposeValidItemName(message.Subject),
                                emailHistoryTemplate);
                            newItem[Constants.Fields.EmailRequestHistory.Message] = message.ToString();

                            historyBucket.Editing.EndEdit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Add", ex, typeof (EmailRequestHistory));
            }
        }
    }
}