using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using PostmarkDotNet;
using PostmarkDotNet.Legacy;
using PostmarkDotNet.Model;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using ThreeManCrew.ContentByMail.Common;
using ThreeManCrew.ContentByMail.Core.ContentEmailManager;

namespace ThreeManCrew.ContentByMail.Core.Notifications
{
    internal static class NotificationManager
    {
        /// <summary>
        ///     Sends the specified message.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="message">The message.</param>
        /// <param name="notificationMessageType"></param>
        internal static void Send(string to, NotificationMessage message, NotificationMessageType notificationMessageType)
        {
            Assert.ArgumentNotNull(message, "message");

            try
            {
                if (String.IsNullOrEmpty(to)) to = ContentEmailManagerTemplate.FallBackAddress;

                string subject;
                string body;
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

                if (message.SendUsingPostMark.HasValue)
                    SendUsingPostMark(message.Sender, to, subject, body, message.SendUsingPostMark.ServerApi);
                else
                    SendUsingSmtp(message.Sender, to, subject, body);
            }
            catch (Exception ex)
            {
                Log.Error("Cannot send notification.", ex, typeof(NotificationManager));
            }
        }

        /// <summary>
        ///     Sends the specified messages.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <param name="to"></param>
        /// <param name="message"></param>
        /// <param name="notificationMessageTypes"></param>
        internal static void Send(string to, NotificationMessage message,
            IEnumerable<NotificationMessageType> notificationMessageTypes)
        {
            Assert.ArgumentNotNull(notificationMessageTypes, "Notification Types");

            foreach (var errorTypes in notificationMessageTypes)
            {
                Send(to, message, errorTypes);
            }
        }

        internal static void SendUsingPostMark(string sender, string to, string subject, string body, string serverApi)
        {
            var pmMessage = new PostmarkMessage
            {
                From = sender,
                To = to,
                Subject = subject,
                HtmlBody = body,
                TrackOpens = true,
                Headers = new HeaderCollection()
            };

            var pmClient = new PostmarkClient(serverApi);
            var response = pmClient.SendMessage(pmMessage);

            if (response.Status != PostmarkStatus.Success)
            {
                Log.Error("Response was: " + response.Message, response);
            }
        }

        internal static void SendUsingSmtp(string sender, string to, string subject, string body)
        {
            using (var client = new SmtpClient(Settings.MailServer, Settings.MailServerPort))
            {
                client.Credentials = new NetworkCredential(Settings.MailServerUserName, Settings.MailServerPassword);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;                

                var mail = new MailMessage(sender, to, subject, body)
                {
                    BodyEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8,
                    IsBodyHtml = true
                };

                client.SendAsync(mail, null);
            }
        }
    }
}