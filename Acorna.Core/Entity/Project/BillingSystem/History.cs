using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    public class History : BaseEntity
    {
        public string OldUserName { get; set; }
        public string NewUserName { get; set; }
        public int PhoneNumber { get; set; }
    }
}
