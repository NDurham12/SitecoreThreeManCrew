namespace ContentByMail.Core.Notification
{
    using Sitecore.Configuration;
    using Sitecore.Diagnostics;
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
    }
}
