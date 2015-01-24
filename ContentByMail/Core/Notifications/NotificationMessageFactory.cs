namespace ContentByMail.Core.Notifications
{
    using ContentByMail.Common;
    using ContentByMail.Common.Enumerations;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Items;

    internal class NotificationMessageFactory
    {       
        public NotificationMessage CreateMessage(ID itemId)
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
