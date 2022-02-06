using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.Models.Project.BillingSystem
{
    public class HistoryModel : BaseModel
    {
        public string OldUserName { get; set; }
        public string NewUserName { get; set; }
        public int PhoneNumber { get; set; }
    }
}
