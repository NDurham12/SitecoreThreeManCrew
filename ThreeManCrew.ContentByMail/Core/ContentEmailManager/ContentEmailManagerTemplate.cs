﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using ThreeManCrew.ContentByMail.Common;
using ThreeManCrew.ContentByMail.Core.EmailProcessor;
using ThreeManCrew.ContentByMail.Core.Notifications;

namespace ThreeManCrew.ContentByMail.Core.ContentEmailManager
{
    public static class ContentEmailManagerTemplate
    {

        private static readonly Item item;

        public static string FallBackAddress { get; set; }
        public static NotificationMessage DefaultMessage { get; set; }
        public static IEnumerable<Item> EmailTemplates { get; set; }      

        static ContentEmailManagerTemplate()
        {
            item = Factory.GetDatabase(Constants.Databases.Web).GetItem(Constants.Settings.ContentByEmailModuleItem);

            if (item != null)
            {
                FallBackAddress = item[Constants.Fields.MailManager.FallbackNotificationAddress];

                var defaultNotificationTemplateId = new ID(item[Constants.Fields.MailManager.DefaultNotificationTemplate]);

                DefaultMessage = new NotificationMessage(Sitecore.Context.Database.GetItem(defaultNotificationTemplateId));

                EmailTemplates = item.Children.Where(i => i.IsDerived(Constants.Templates.EmailProcessorTemplate));
            }
        }
    }
}