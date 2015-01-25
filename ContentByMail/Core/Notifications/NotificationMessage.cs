using ContentByMail.Common;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace ContentByMail.Core.Notifications
{
    public class NotificationMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NotificationMessage" /> class.
        /// </summary>
        /// <param name="template">The template.</param>
        internal NotificationMessage(Item template)
        {
            Assert.ArgumentNotNull(template, "template");

            Sender = template[Constants.Fields.Notification.Sender];
            // this.SendUsingPostMark = template[Constants.Fields.Notification.SendUsingPostMark];

            SuccessSubject = template[Constants.Fields.Notification.SucessSubject];
            SuccessBody = template[Constants.Fields.Notification.SuccessBody];

            InvalidTemplateSubject = template[Constants.Fields.Notification.InvalidTemplateSubject];
            InvalidTemplateBody = template[Constants.Fields.Notification.InvalidTemplateBody];

            InvalidFieldSubject = template[Constants.Fields.Notification.InvalidFieldTokenSubject];
            InvalidFieldBody = template[Constants.Fields.Notification.InvalidFieldTokenBody];

            GenericFailureSubject = template[Constants.Fields.Notification.GenericFailureNotificationSubject];
            GenericFailureBody = template[Constants.Fields.Notification.GenericFailureNotificationBody];
        }

        /// <summary>
        ///     Gets or sets the sender.
        /// </summary>
        internal string Sender { get; set; }

        /// <summary>
        ///     Gets or sets the subject.
        /// </summary>
        internal string SuccessBody { get; set; }

        /// <summary>
        ///     Gets or sets the success subject.
        /// </summary>
        internal string SuccessSubject { get; set; }

        /// <summary>
        ///     Gets or sets the invalid template body.
        /// </summary>
        internal string InvalidTemplateBody { get; set; }

        /// <summary>
        ///     Gets or sets the invalid template subject.
        /// </summary>
        internal string InvalidTemplateSubject { get; set; }

        /// <summary>
        ///     Gets or sets the invalid field body.
        /// </summary>
        internal string InvalidFieldBody { get; set; }

        /// <summary>
        ///     Gets or sets the invalid field subject.
        /// </summary>
        internal string InvalidFieldSubject { get; set; }

        /// <summary>
        ///     Gets or sets the generic failure body.
        /// </summary>
        /// <value>
        ///     The generic failure body.
        /// </value>
        internal string GenericFailureBody { get; set; }

        /// <summary>
        ///     Gets or sets the generic failure subject.
        /// </summary>
        internal string GenericFailureSubject { get; set; }

        internal bool SendUsingPostMark { get; set; }

        /// <summary>
        ///     Gets or sets the body.
        /// </summary>
        internal string Body { get; set; }
    }
}