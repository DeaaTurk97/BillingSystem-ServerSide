using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.Models.Project.BillingSystem
{
    public class PlanServiceModel 
    {

        public float Limit { get; set; }
        public string Unit { get; set; }
        public string AdditionalUnit { get; set; }
        public float AdditionalUnitPrice { get; set; }
        public int PlanService { get; set; }
    }
}
