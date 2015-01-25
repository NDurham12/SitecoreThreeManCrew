using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using ThreeManCrew.ContentByMail.Common;

namespace ThreeManCrew.ContentByMail.Core.Notifications
{


    internal class NotificationMessageFactory
    {
        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <returns>The message object.</returns>
        internal NotificationMessage CreateMessage(ID itemId)
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
