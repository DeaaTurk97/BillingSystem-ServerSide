using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Notification
{
    public class NotificationsDetails : BaseEntity
    {
        public string MessageText { get; set; }
        public int NotificationsId { get; set; }
    }
}
