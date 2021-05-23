using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Notification
{
    public class NotificationItem : BaseEntity
    {
        public int RecipientId { get; set; }
        public bool IsRead { get; set; }
        public bool Deleted { get; set; }
        public int NotificationTypeId { get; set; }
        [ForeignKey("NotificationTypeId")]
        public NotificationType NotificationType { get; set; }

        public virtual ICollection<NotificationItemTranslation> NotificationItemTranslations { get; set; }
        public int ReferenceMassageId { get; set; }
    }

    public class NotificationItemTranslation : TranslateBase
    {
        public int NotificationItemId { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string MessageText { get; set; }
    }
}
