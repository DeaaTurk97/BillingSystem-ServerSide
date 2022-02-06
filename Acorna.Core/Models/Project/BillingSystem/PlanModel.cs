using Acorna.Core.Entity.Project.BillingSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.Models.Project.BillingSystem
{
    public class PlanModel : BaseModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

        public IEnumerable<PlanServiceModel> PlanServices { get; set; }
    }
}
