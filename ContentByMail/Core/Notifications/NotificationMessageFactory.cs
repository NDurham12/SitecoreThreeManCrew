using ContentByMail.Common;
using Sitecore.Configuration;
using Sitecore.Data;

namespace ContentByMail.Core.Notifications
{
    internal class NotificationMessageFactory
    {
        /// <summary>
        ///     Creates the message.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <returns>The message object.</returns>
        internal NotificationMessage CreateMessage(ID itemId)
        {
            NotificationMessage message = null;
            var item = Factory.GetDatabase(Constants.Databases.Web).GetItem(itemId);

            if (item != null)
            {
                message = new NotificationMessage(item);
            }

            return message;
        }
    }
}