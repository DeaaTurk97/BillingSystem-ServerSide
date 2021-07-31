using Acorna.Core.Entity.Notification;
using Acorna.Core.Models.Notification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Repository.Notification
{
    public interface INotificationRepository : IRepository<Notifications>
    {
        int AddNotificationItem(NotificationItemModel model);
        Task<IEnumerable<NotificationItemModel>> GetNewNumbersAndBills(string currentUserRole);
    }
}
