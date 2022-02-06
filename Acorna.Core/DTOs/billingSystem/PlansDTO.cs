using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.DTOs.billingSystem
{
    internal class PlansDTO : BaseDTO
    {
        public string PlanCode { get; set; }
        public string PlanDescription { get; set; }
        public int ServiceId { get; set; }
        public float PlanPrice { get; set; }
     
    }
}
