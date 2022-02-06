using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.Models.Project.BillingSystem
{
    public class PlanServiceModel 
    {

        public int Limit { get; set; }
        public string Unit { get; set; }
        public string AdditionalUnit { get; set; }
        public int AdditionalUnitPrice { get; set; }
        public int PlanService { get; set; }
    }
}
