using Acorna.Core.Entity.Security;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    [Table("AllocatedUsersServices")]
    public class AllocatedUsersService : BaseEntity
    {
        public int ServiceUsedId { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ServiceUsed ServiceUsed { get; set; }
    }
}
