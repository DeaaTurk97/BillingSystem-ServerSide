using Acorna.Core.Entity.Notification;
using Acorna.Core.Models.Notification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.Repository.Notification
{
    public interface INotificationRepository : IRepository<NotificationItem>
    {
        int AddNotificationItem(NotificationItemModel model);
    }
}
