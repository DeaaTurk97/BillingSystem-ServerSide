using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    [Table("SimProfiles")]
    public class SimProfile : BaseEntity
    {
        public string ProfileName { get; set; }
    }
}
