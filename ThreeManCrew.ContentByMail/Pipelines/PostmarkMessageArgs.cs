using System.Collections.Generic;
using PostmarkDotNet;
using Sitecore.Pipelines;
using ThreeManCrew.ContentByMail.Common;
using ThreeManCrew.ContentByMail.Core.Managers;
using ThreeManCrew.ContentByMail.Core.Templates;

namespace ThreeManCrew.ContentByMail.Pipelines
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
                NotificationManager.Send(ContentEmailManagerTemplate.FallBackAddress, ContentEmailManagerTemplate.DefaultMessage, NotificationMessageType.InvalidTemplate);
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