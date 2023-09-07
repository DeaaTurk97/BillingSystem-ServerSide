using Acorna.Core.Models.Notification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.IServices.Notification
{
    public interface INotificationService
    {
        List<NotificationItemModel> GetAllNotificationItems();
        List<NotificationItemModel> GetAllNotificationItems(int userId);
        List<NotificationItemModel> GetAllUnreadNotificationItems();
        IEnumerable<NotificationItemModel> GetUnreadNotification(int userId);
        NotificationItemModel GetNotificationItem(int id);
        int AddNotificationItem(NotificationItemModel model);
        bool ReadNotificationItem(int notificationId);
        bool UpdateReadNewNotification(int receiverId, int senderId, int notificationTypeId, int notificationId);
        bool DeleteNotificationItem(int id);
        Task<IEnumerable<NotificationItemModel>> GetNewNumbersAndBillsByRoleId(string currentUserRole);
        bool UpdateReadNewNumbersAndBills(string currentUserRole, int notificationTypeId);
    }
}
