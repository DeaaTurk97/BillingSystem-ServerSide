﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Notification
{
    public class NotificationType : BaseEntity
    {
        [Column(TypeName = "nvarchar(250)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }
        public virtual List<Notifications> Notifications { get; set; }
    }
}
