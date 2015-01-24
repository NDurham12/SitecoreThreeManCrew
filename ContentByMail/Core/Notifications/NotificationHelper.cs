using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentByMail.Common.Enumerations;
using ContentByMail.Pipelines;

namespace ContentByMail.Core.Notifications
{
    public static class NotificationHelper
    {
        public static void SendNotification(PostmarkMessageArgs messageArgs)
        {            
            NotificationMessageFactory factory = new NotificationMessageFactory();

            //NotificationMessage message = factory.CreateMessage(NotificationMessageType.Failure);
            //NotificationManager manager = new NotificationManager();
            
            //manager.Send(message);
        }
    }
}
