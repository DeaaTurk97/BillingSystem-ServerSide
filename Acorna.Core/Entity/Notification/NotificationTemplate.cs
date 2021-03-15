using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Notification
{
    public class NotificationTemplate : BaseEntity
    {
        public int NotificationTypeId { get; set; }
    }
}
