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
using ThreeManCrew.ContentByMail.Core.Templates;

namespace ThreeManCrew.ContentByMail.Core.Managers
{
    internal static class NotificationManager
    {
        /// <summary>
        ///     Sends the specified message.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="messageTemplate">The message.</param>
        /// <param name="notificationMessageType"></param>
        internal static void Send(string to, NotificationMessageTemplate messageTemplate, NotificationMessageType notificationMessageType)
        {
            Assert.ArgumentNotNull(messageTemplate, "message");

            try
            {
                if (String.IsNullOrEmpty(to)) to = ContentEmailManagerTemplate.FallBackAddress;

                string subject;
                string body;
                switch (notificationMessageType)
                {
                    case NotificationMessageType.Success:
                        subject = messageTemplate.SuccessSubject;
                        body = messageTemplate.SuccessBody;
                        break;
                    case NotificationMessageType.InvalidTemplate:
                        subject = messageTemplate.InvalidTemplateSubject;
                        body = messageTemplate.InvalidTemplateBody;
                        break;
                    case NotificationMessageType.InvalidField:
                        subject = messageTemplate.InvalidFieldSubject;
                        body = messageTemplate.InvalidFieldBody;
                        break;
                    default:
                        subject = messageTemplate.GenericFailureSubject;
                        body = messageTemplate.GenericFailureBody;
                        break;
                }

                if (messageTemplate.SendUsingPostMark.HasValue)
                    SendUsingPostMark(messageTemplate.Sender, to, subject, body, messageTemplate.SendUsingPostMark.ServerApi);
                else
                    SendUsingSmtp(messageTemplate.Sender, to, subject, body);
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
        /// <param name="messageTemplate"></param>
        /// <param name="notificationMessageTypes"></param>
        internal static void Send(string to, NotificationMessageTemplate messageTemplate,
            IEnumerable<NotificationMessageType> notificationMessageTypes)
        {
            Assert.ArgumentNotNull(notificationMessageTypes, "Notification Types");

            foreach (var errorTypes in notificationMessageTypes)
            {
                Send(to, messageTemplate, errorTypes);
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