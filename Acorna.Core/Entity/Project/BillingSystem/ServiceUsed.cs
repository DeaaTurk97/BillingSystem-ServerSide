﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    [Table("ServicesUsed")]
    public class ServiceUsed : BaseEntity
    {
        [Required, MaxLength(150)]
        public string ServiceUsedNameAr { get; set; }
        [Required, MaxLength(150)]
        public string ServiceUsedNameEn { get; set; }
        public decimal? ServicePrice { get; set; }
        public bool IsCalculatedValue { get; set; }
        public bool IsNeedApproved { get; set; }
        public bool NonOfficial { get; set; }

        public virtual ICollection<PlanService> PlanServices { get; set; }
        public virtual ICollection<AllocatedUsersService> AllocatedUsersServices { get; set; }
    }
}
