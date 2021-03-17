using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    [Table("Groups")]
    public class Group : BaseEntity
    {
        [Required, MaxLength(150)]
        public string GroupNameAr { get; set; }
        [Required, MaxLength(150)]
        public string GroupNameEn { get; set; }
    }
}
