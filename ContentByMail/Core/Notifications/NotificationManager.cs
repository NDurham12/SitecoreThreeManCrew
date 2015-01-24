namespace ContentByMail.Core.Notifications
{
    using ContentByMail.Common;
    using Sitecore.Configuration;
    using Sitecore.Data.Items;
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
        /// <param name="message">The message.</param>
        internal void Send(NotificationMessage message)
        {
            Assert.ArgumentNotNull(message, "message");

            try
            {
                if(String.IsNullOrEmpty(message.Recipient))
                {
                    Item mailmanager = Factory.GetDatabase(Constants.Databases.Web).GetItem(Constants.Items.ContentMailManager);

                    if(mailmanager != null)
                    {
                        message.Recipient = mailmanager[Constants.Fields.MailManager.FallbackNotificationAddress];
                    }
                }

                using (SmtpClient client = new SmtpClient(Settings.MailServer, Settings.MailServerPort))
                {
                    client.Credentials = new NetworkCredential(Settings.MailServerUserName, Settings.MailServerPassword);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    MailMessage mail = new MailMessage(message.Sender, message.Recipient, message.Subject, message.Body)
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
        internal void Send(IEnumerable<NotificationMessage> messages)
        {
            Assert.ArgumentNotNull(messages, "messages");

            foreach (NotificationMessage message in messages)
            {
                this.Send(message);
            }
        }
    }
}
