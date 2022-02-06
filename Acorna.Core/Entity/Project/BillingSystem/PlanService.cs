using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    public class PlanService : BaseEntity
    {
        public int PlanId { get; set; }

        [ForeignKey(nameof(PlanId))]
        public Plan Plan { get; set; }
        public int ServiceUsedId { get; set; }
        [ForeignKey(nameof(ServiceUsedId))]
        public ServiceUsed ServiceUsed { get; set; }

        public int Limit { get; set; }
        public string Unit { get; set; }
        public string AdditionalUnit { get; set; }
        public int AdditionalUnitPrice { get; set; }
    }
}
