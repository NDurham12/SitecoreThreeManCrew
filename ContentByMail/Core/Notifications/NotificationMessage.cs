namespace ContentByMail.Core.Notifications
{
    using ContentByMail.Common;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    public class NotificationMessage
    {        
        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        internal string Sender { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        internal string SuccessBody { get; set; }

        /// <summary>
        /// Gets or sets the success subject.
        /// </summary>
        internal string SuccessSubject { get; set; }

        /// <summary>
        /// Gets or sets the invalid template body.
        /// </summary>
        internal string InvalidTemplateBody { get; set; }

        /// <summary>
        /// Gets or sets the invalid template subject.
        /// </summary>
        internal string InvalidTemplateSubject { get; set; }

        /// <summary>
        /// Gets or sets the invalid field body.
        /// </summary>
        internal string InvalidFieldBody { get; set; }

        /// <summary>
        /// Gets or sets the invalid field subject.
        /// </summary>
        internal string InvalidFieldSubject { get; set; }

        /// <summary>
        /// Gets or sets the generic failure body.
        /// </summary>
        /// <value>
        /// The generic failure body.
        /// </value>
        internal string GenericFailureBody { get; set; }

        /// <summary>
        /// Gets or sets the generic failure subject.
        /// </summary>
        internal string GenericFailureSubject { get; set; }

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
            this.SuccessSubject = template[Constants.Fields.Notification.SucessSubject];
            this.SuccessBody = template[Constants.Fields.Notification.SuccessBody];

            this.InvalidTemplateSubject = template[Constants.Fields.Notification.InvalidTemplateSubject];
            this.InvalidTemplateBody = template[Constants.Fields.Notification.InvalidTemplateBody];

            this.InvalidFieldSubject = template[Constants.Fields.Notification.InvalidFieldTokenSubject];
            this.InvalidFieldBody = template[Constants.Fields.Notification.InvalidFieldTokenBody];

            this.GenericFailureSubject = template[Constants.Fields.Notification.GenericFailureNotificationSubject];
            this.GenericFailureBody = template[Constants.Fields.Notification.GenericFailureNotificationBody];
        }
    }
}
