using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Web;

namespace ContentByMail.Common
{
    internal static class ItemExtensions
    {
        private const string DerivedCacheKey = "{0},{1}";
        private static readonly Dictionary<string, bool> DerivedCache = new Dictionary<string, bool>();

        /// <summary>
        ///     Get a field value as string
        /// </summary>
        /// <param name="item"> </param>
        /// <param name="fieldId"> </param>
        /// <returns> </returns>
        public static string GetString(this Item item, ID fieldId)
        {
            return item[fieldId];
        }

        /// <summary>
        ///     Set a string as field value
        /// </summary>
        /// <param name="item"> </param>
        /// <param name="fieldId"> </param>
        /// <param name="value"> </param>
        /// <returns> </returns>
        public static void SetString(this Item item, ID fieldId, string value)
        {
            item[fieldId] = value;
        }

        /// <summary>
        ///     Get value from a CheckboxField
        /// </summary>
        /// <param name="item"> </param>
        /// <param name="fieldId"> </param>
        /// <returns> </returns>
        public static bool GetCheckBoxValue(this Item item, ID fieldId)
        {
            return MainUtil.GetBool(item[fieldId], default(Boolean));
        }

        /// <summary>
        ///     Get namevaluecollection from from a NameValueList
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public static NameValueCollection GetNameValueList(this Item item, ID fieldId)
        {
            var urlParamsToParse = item[fieldId];
            return WebUtil.ParseUrlParameters(urlParamsToParse);
        }

        /// <summary>
        ///     Determines whether the specified Item is derived from the specified TemplateItem.
        /// </summary>
        /// <param name="item"> The Item. </param>
        /// <param name="template"> The TemplateItem. </param>
        /// <returns> <c>true</c> if the specified Item is derived from the specified TemplateItem; otherwise, <c>false</c> . </returns>
        internal static bool IsDerived(this Item item, TemplateItem template)
        {
            return item.IsDerived(template.ID);
        }

        /// <summary>
        ///     Determines whether the specified Item is derived from the specified template ID.
        /// </summary>
        /// <param name="item"> The Item. </param>
        /// <param name="templateId"> The template ID. </param>
        /// <returns> <c>true</c> if the specified Item is derived from the specified template ID; otherwise, <c>false</c> . </returns>
        internal static bool IsDerived(this Item item, ID templateId)
        {
            return IsDerived(templateId, item.Template);
        }

        /// <summary>
        ///     Determines whether the specified Item is within the hierarchy of the current Site.
        /// </summary>
        /// <param name="item"> The item. </param>
        /// <returns> <c>true</c> if the specified Item is within the hierarchy of the current Site; otherwise, <c>false</c> . </returns>
        internal static bool IsStandardValuesItem(this Item item)
        {
            return item.Name.Equals(Sitecore.Constants.StandardValuesItemName,
                StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        ///     Get selected items from a drop link
        /// </summary>
        /// <param name="item"> </param>
        /// <param name="fieldId"> </param>
        public static Item GetDropLinkSelectedItem(this Item item, ID fieldId)
        {
            Item linkedItem = null;
            var field = new LinkField(item.Fields[fieldId]);

            if (field != null)
            {
                linkedItem = field.TargetItem;
            }

            return linkedItem;
        }

        private static bool IsDerived(ID templateId, TemplateItem template)
        {
            if (template == null)
                return false;

            if (ID.IsNullOrEmpty(templateId))
                return false;

            var cacheKey = String.Format(DerivedCacheKey, templateId, template.ID);

            lock (DerivedCache)
            {
                if (DerivedCache.ContainsKey(cacheKey))
                    return DerivedCache[cacheKey];
            }

            var derived = (template.ID == templateId) ||
                          template.BaseTemplates.Any(baseTemplate => IsDerived(templateId, baseTemplate));

            lock (DerivedCache)
            {
                if (!DerivedCache.ContainsKey(cacheKey))
                {
                    DerivedCache.Add(cacheKey, derived);
                }
            }

            return derived;
        }
    }
}