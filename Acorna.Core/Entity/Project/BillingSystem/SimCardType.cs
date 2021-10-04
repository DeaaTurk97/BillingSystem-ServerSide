using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    [Table("SimCardTypes")]
    public class SimCardType : BaseEntity
    {
        [Required, MaxLength(150)]
        public string TypeName { get; set; }
    }
}
