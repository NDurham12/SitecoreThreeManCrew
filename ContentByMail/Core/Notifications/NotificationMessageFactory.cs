namespace ContentByMail.Core.Notifications
{
    using ContentByMail.Common;
    using ContentByMail.Common.Enumerations;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Items;

    internal class NotificationMessageFactory
    {
        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The message object.</returns>
        internal NotificationMessage CreateMessage(NotificationMessageType type)
        {
            NotificationMessage message = null;

            switch (type)
            {
                case NotificationMessageType.Success:
                    message = CreateMessage(Constants.Settings.NotificationOnSuccessItemId);
                    break;
                case NotificationMessageType.Failure:
                    message = CreateMessage(Constants.Settings.NotificationOnFailureItemId);
                    break;
                default:
                    break;
            }

            return message;
        }

        private NotificationMessage CreateMessage(ID itemId)
        {
            NotificationMessage message = null;
            Item item = Factory.GetDatabase(Constants.Databases.Web).GetItem(itemId);

            if (item != null)
            {
                message = new NotificationMessage(item);
            }

            return message;
        }
    }
}
