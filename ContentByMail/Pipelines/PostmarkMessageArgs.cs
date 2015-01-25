using ContentByMail.Common;
using ContentByMail.Common.Enumerations;
using ContentByMail.Core.EmailProcessor;
using ContentByMail.Core.Notifications;
using PostmarkDotNet;
using Sitecore.Pipelines;
using System.Collections.Generic;

namespace ContentByMail.Pipelines
{
    public class PostmarkMessageArgs : PipelineArgs
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public new PostmarkInboundMessage Message { get; set; }

        /// <summary>
        /// Gets or sets the message token values.
        /// </summary>
        public Dictionary<string, string> MessageTokenValues { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostmarkMessageArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public PostmarkMessageArgs(PostmarkInboundMessage message)
        {
            this.MessageTokenValues = EmailParser.ParseTokens(message);

            if (!this.MessageTokenValues.ContainsKey("Template"))
            {
                NotificationManager manager = new NotificationManager();
                manager.Send(Constants.DefaultContentModule.FallBackAddress, Constants.DefaultContentModule.DefaultMessage, NotificationMessageType.InvalidTemplate);

                this.AbortPipeline();
            }

            this.Message = message;
        }
    }
}
