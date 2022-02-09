using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.Models.Project.BillingSystem
{
    public class HistoryModel : BaseModel
    {
        public string UserName { get; set; }
        public string OldUserName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? EffectiveDate { get; set; }
    }
}
