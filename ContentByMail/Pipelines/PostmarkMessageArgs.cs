using System.Collections.Generic;
using ContentByMail.Common;
using ContentByMail.Common.Enumerations;
using ContentByMail.Core.EmailProcessor;
using ContentByMail.Core.Notifications;
using PostmarkDotNet;
using Sitecore.Pipelines;

namespace ContentByMail.Pipelines
{
    public class PostmarkMessageArgs : PipelineArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PostmarkMessageArgs" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public PostmarkMessageArgs(PostmarkInboundMessage message)
        {
            MessageTokenValues = EmailParser.ParseTokens(message);

            if (!MessageTokenValues.ContainsKey("Template"))
            {
                var manager = new NotificationManager();
                manager.Send(Constants.DefaultContentModule.FallBackAddress,
                    Constants.DefaultContentModule.DefaultMessage, NotificationMessageType.InvalidTemplate);

                AbortPipeline();
            }

            Message = message;
        }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        public new PostmarkInboundMessage Message { get; set; }

        /// <summary>
        ///     Gets or sets the message token values.
        /// </summary>
        public Dictionary<string, string> MessageTokenValues { get; set; }
    }
}