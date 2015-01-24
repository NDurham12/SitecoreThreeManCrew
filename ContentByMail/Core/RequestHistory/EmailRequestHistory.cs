using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentByMail.Common;
using PostmarkDotNet;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Data.Templates;
using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;

namespace ContentByMail.Core.RequestHistory
{
    public class EmailRequestHistory
    {
        /// <summary>
        /// Adds a new message to the Email Request history item bucket
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fullMessage"></param>
        public void Add(PostmarkInboundMessage message)
        {
            var masterDb = Factory.GetDatabase(Constants.Databases.Master);

            try
            {
                if (masterDb != null)
                {
                    var historyBucket = masterDb.GetItem(Constants.Settings.EmailRequestHistoryItemId);
                    TemplateItem emailHistoryTemplate = masterDb.GetItem(Constants.Templates.EmailContentRequestHistory);

                    if (historyBucket != null && emailHistoryTemplate != null)
                    {
                        using (new UserSwitcher(Constants.Security.ServiceUser))
                        {
                            historyBucket.Editing.BeginEdit();
                            var newItem = historyBucket.Add(ItemUtil.ProposeValidItemName(message.Subject), emailHistoryTemplate);
                            newItem.Fields[Constants.Fields.EmailRequestHistory.Message].Value = Serialize(message);
                            historyBucket.Editing.EndEdit();
                        }                                        
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("EmailRequestHistory", ex, this);
            }
        }

        public static string Serialize(object objectToSerialize)
        {
            try
            {
                var rawData = new System.Xml.Serialization.XmlSerializer(objectToSerialize.GetType());
                var ms = new StringWriter();
                rawData.Serialize(ms, objectToSerialize);
                return ms.ToString();

            }
            catch (Exception ex)
            {
                Log.Error("EmailRequestHistory", typeof (EmailRequestHistory));
            }

            return "";
        }
    }
}
