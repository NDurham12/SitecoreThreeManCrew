using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using ThreeManCrew.ContentByMail.Common;

namespace ThreeManCrew.ContentByMail.Core.Templates
{
    public class PostMarkProviderTemplate
    {
        internal string ServerName { get; set; }

        internal string ServerApi { get; set; }

        internal string EMailAddress { get; set; }

        public bool HasValue { get; set; }

        public PostMarkProviderTemplate(string templateId)
        {
            var template = Sitecore.Context.ContentDatabase.GetItem(templateId);
            if (template != null)
            {                
                ServerName = template[Constants.Fields.EmailProvider.ServerName];
                ServerApi = template[Constants.Fields.EmailProvider.ServerApi];
                EMailAddress = template[Constants.Fields.EmailProvider.EMailAddress];
                HasValue = true;
            }
            else
            {
                HasValue = false;
            }
        }
    }
}