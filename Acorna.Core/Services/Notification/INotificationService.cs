using System.Collections.Generic;
using Acorna.Core.Models.Notification;

namespace Acorna.Core.IServices.Notification
{
    public interface INotificationService
    {
        List<NotificationItemModel> GetAllNotificationItems();
        List<NotificationItemModel> GetAllNotificationItems(int userId);
        List<NotificationItemModel> GetAllUnreadNotificationItems();
        IEnumerable<NotificationItemModel> GetAllUnreadNotificationItems(int userId);
        NotificationItemModel GetNotificationItem(int id);
        int AddNotificationItem(NotificationItemModel model);
        bool ReadNotificationItem(int notificationId);
        bool UpdateReadNotificationsItems(int receiverId, int senderId, int notificationTypeId);
        bool DeleteNotificationItem(int id);
    }
}
