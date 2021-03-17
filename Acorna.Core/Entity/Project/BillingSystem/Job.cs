using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    [Table("Jobs")]
    public class Job : BaseEntity
    {
        [Required, MaxLength(150)]
        public string JobNameAr { get; set; }
        [Required, MaxLength(150)]
        public string JobNameEn { get; set; }
    }
}
