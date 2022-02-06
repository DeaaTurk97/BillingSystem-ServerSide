using Acorna.Core.Entity.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PlanService> PlanServices { get; set; }

        public virtual ICollection<User> User { get; set; }
        public float Price { get; set; }

    }
}
