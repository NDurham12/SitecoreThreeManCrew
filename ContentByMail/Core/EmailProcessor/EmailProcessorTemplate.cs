﻿using System.Collections.Generic;
using ContentByMail.Common;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace ContentByMail.Core.EmailProcessor
{
    public class EmailProcessorTemplate
    {
            /// <summary>
            /// Gets or sets the Id.
            /// </summary>
            internal string Id { get; set; }

            /// <summary>
            /// Gets or sets the Name.
            /// </summary>
            internal string Name { get; set; }

            /// <summary>
            /// Gets or sets the EmailTokens.
            /// </summary>
            internal IEnumerable<EmailProcessorTemplateToken> EmailTokens  { get; set; }


            /// <summary>
            /// Initializes a new instance of the <see cref="EmailProcessorTemplate"/> class.
            /// </summary>
            /// <param name="item">The template.</param>
            internal EmailProcessorTemplate(Item item)
            {
                Assert.ArgumentNotNull(item, "EmailProcessorTemplate item is null");

                Assert.IsTrue(item.IsDerived(Constants.Templates.EmailProcessorTemplate), "EmailProcessorTemplate derives from wrong template");

                Id = item.ID.ToString();

                Name = item.Name;

                EmailTokens = EmailProcessorTemplateTokenFactory.CreateCollection(item.GetMultiListValues(Constants.Fields.EmailProcessorTemplate.EmailProcessorTemplateTokenList));

            }
       
    }
}