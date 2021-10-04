using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.Models.Project.BillingSystem
{
    public class AllocatedUsersServiceModel : BaseModel
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }
    }
}
