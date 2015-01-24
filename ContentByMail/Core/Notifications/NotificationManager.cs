namespace ContentByMail.Core.Notifications
{
    using ContentByMail.Common;
    using ContentByMail.Common.Enumerations;
    using Sitecore.Configuration;
    using Sitecore.Diagnostics;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    internal class NotificationManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationManager"/> class.
        /// </summary>
        internal NotificationManager() { }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="message">The message.</param>
        /// <param name="notificationMessageType"></param>
        internal void Send(string to, NotificationMessage message, NotificationMessageType notificationMessageType)
        {
            Assert.ArgumentNotNullOrEmpty(to, "to");
            Assert.ArgumentNotNull(message, "message");
            Assert.ArgumentNotNull(notificationMessageType, "notificationMessageType");

            try
            {
                if (String.IsNullOrEmpty(to))
                {
                    to = Constants.DefaultContentModule.FallBackAddress;
                }

                using (SmtpClient client = new SmtpClient(Settings.MailServer, Settings.MailServerPort))
                {
                    client.Credentials = new NetworkCredential(Settings.MailServerUserName, Settings.MailServerPassword);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = Constants.Settings.ContentByEmailEmailEnableSsl;

                    string subject, body;

                    switch (notificationMessageType)
                    {
                        case NotificationMessageType.Success:
                            subject = message.SuccessSubject;
                            body = message.SuccessBody;
                            break;
                        case NotificationMessageType.InvalidTemplate:
                            subject = message.InvalidTemplateSubject;
                            body = message.InvalidTemplateBody;
                            break;
                        case NotificationMessageType.InvalidField:
                            subject = message.InvalidFieldSubject;
                            body = message.InvalidFieldBody;
                            break;
                        default:
                            subject = message.GenericFailureSubject;
                            body = message.GenericFailureBody;
                            break;
                    }

                    MailMessage mail = new MailMessage(message.Sender, to, subject, body)
                    {
                        BodyEncoding = Encoding.UTF8,
                        SubjectEncoding = Encoding.UTF8,
                        IsBodyHtml = true
                    };

                    client.SendAsync(mail, userToken: null);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Cannot send notification.", ex, this);
            }
        }

        /// <summary>
        /// Sends the specified messages.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <param name="to"></param>
        /// <param name="message"></param>
        /// <param name="notificationMessageTypes"></param>
        internal void Send(string to, NotificationMessage message, IEnumerable<NotificationMessageType> notificationMessageTypes)
        {
            Assert.ArgumentNotNullOrEmpty(to, "to");
            Assert.ArgumentNotNull(message, "message");
            Assert.ArgumentNotNull(notificationMessageTypes, "notificationMessageTypes");

            foreach (NotificationMessageType errorTypes in notificationMessageTypes)
            {
                this.Send(to, message, errorTypes);
            }
        }
    }
}
