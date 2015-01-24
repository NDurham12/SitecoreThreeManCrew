namespace ContentByMail.Core.Notifications
{
    using ContentByMail.Common;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    internal class NotificationMessage
    {
        /// <summary>
        /// Gets or sets the recipient.
        /// </summary>
        internal string Recipient { get; set; }

        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        internal string Sender { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        internal string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        internal string Body { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationMessage"/> class.
        /// </summary>
        /// <param name="template">The template.</param>
        internal NotificationMessage(Item template)
        {
            Assert.ArgumentNotNull(template, "template");

            this.Sender = template[Constants.Fields.Notification.Sender];
            this.Subject = template[Constants.Fields.Notification.Subject];
            this.Body = template[Constants.Fields.Notification.Body];
        }
    }
}
