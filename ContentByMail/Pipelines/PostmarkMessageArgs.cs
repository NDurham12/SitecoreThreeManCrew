using System.Collections.Generic;
using ContentByMail.Core.EmailProcessor;

namespace ContentByMail.Pipelines
{
    using PostmarkDotNet;
    using Sitecore.Pipelines;

    public class PostmarkMessageArgs : PipelineArgs
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public new PostmarkInboundMessage Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostmarkMessageArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public PostmarkMessageArgs(PostmarkInboundMessage message)
        {
            IEnumerable<EmailProcessorTemplate> emailtemplates = EmailProcessorTemplateFactory.CreateCollection();


            this.Message = message;
        }
    }
}
