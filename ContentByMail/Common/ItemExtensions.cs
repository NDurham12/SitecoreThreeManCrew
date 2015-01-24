namespace ContentByMail.Common
{
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class ItemExtensions
    {
        private const string DerivedCacheKey = "{0},{1}";
        private static readonly Dictionary<string, bool> DerivedCache = new Dictionary<string, bool>();

        #region Strings

        /// <summary>
        ///   Get a field value as string
        /// </summary>
        /// <param name="item"> </param>
        /// <param name="fieldId"> </param>
        /// <returns> </returns>
        public static string GetString(this Item item, ID fieldId)
        {
            return item.Fields[fieldId].Value;
        }

        /// <summary>
        ///   Set a string as field value
        /// </summary>
        /// <param name="item"> </param>
        /// <param name="fieldId"> </param>
        /// <param name="value"> </param>
        /// <returns> </returns>
        public static void SetString(this Item item, ID fieldId, string value)
        {
            item.Fields[fieldId].Value = value;
        }

        #endregion

        #region Lists

        /// <summary>
        ///   Get selected items from a MultilistField
        /// </summary>
        /// <param name="item"> </param>
        /// <param name="fieldId"> </param>
        public static IEnumerable<Item> GetMultiListValues(this Item item, ID fieldId)
        {
            return (new MultilistField(item.Fields[fieldId])).GetItems() ?? Enumerable.Empty<Item>();
        }

        /// <summary>
        ///   Get items from MultilistField, droplink, treelist
        /// </summary>
        /// <param name="item"> </param>
        /// <param name="fieldId"> </param>
        public static IEnumerable<Item> GetItemValues(this Item item, ID fieldId)
        {
            return (new MultilistField(item.Fields[fieldId])).GetItems() ?? Enumerable.Empty<Item>();
        }


        /// <summary>
        ///   Set selected items on a MultilistField field
        /// </summary>
        /// <param name="item"> </param>
        /// <param name="fieldId"> </param>
        /// <param name="items"> </param>
        public static void SetMultiListValues(this Item item, ID fieldId, IEnumerable<Item> items)
        {
            var ids = items.Select(i => i.ID).Select(id => id.ToString().ToUpper());
            var s = string.Join("|", ids.ToArray());
            item.Fields[fieldId].Value = s;
        }


      

        #endregion

        #region Template inheritance

        /// <summary>
        ///   Determines whether the specified Item is derived from the specified TemplateItem.
        /// </summary>
        /// <param name="item"> The Item. </param>
        /// <param name="template"> The TemplateItem. </param>
        /// <returns> <c>true</c> if the specified Item is derived from the specified TemplateItem; otherwise, <c>false</c> . </returns>
        internal static bool IsDerived(this Item item, TemplateItem template)
        {
            return item.IsDerived(template.ID);
        }

        /// <summary>
        ///   Determines whether the specified Item is derived from the specified template ID.
        /// </summary>
        /// <param name="item"> The Item. </param>
        /// <param name="templateId"> The template ID. </param>
        /// <returns> <c>true</c> if the specified Item is derived from the specified template ID; otherwise, <c>false</c> . </returns>
        internal static bool IsDerived(this Item item, ID templateId)
        {
            return IsDerived(templateId, item.Template);
        }

        /// <summary>
        ///   Determines whether the specified Item is within the hierarchy of the current Site.
        /// </summary>
        /// <param name="item"> The item. </param>
        /// <returns> <c>true</c> if the specified Item is within the hierarchy of the current Site; otherwise, <c>false</c> . </returns>
        internal static bool IsStandardValuesItem(this Item item)
        {
            return item.Name.Equals("__standard values", StringComparison.InvariantCultureIgnoreCase);
        }


        #endregion

        private static bool IsDerived(ID templateId, TemplateItem template)
        {
            if (template == null)
                return false;

            if (ID.IsNullOrEmpty(templateId))
                return false;

            var cacheKey = string.Format(DerivedCacheKey, templateId, template.ID);

            lock (DerivedCache)
            {
                if (DerivedCache.ContainsKey(cacheKey))
                    return DerivedCache[cacheKey];
            }

            var derived = template.ID == templateId || template.BaseTemplates.Any(baseTemplate => IsDerived(templateId, baseTemplate));

            lock (DerivedCache)
            {
                if (!DerivedCache.ContainsKey(cacheKey))
                    DerivedCache.Add(cacheKey, derived);
            }

            return derived;
        }
    }
}
