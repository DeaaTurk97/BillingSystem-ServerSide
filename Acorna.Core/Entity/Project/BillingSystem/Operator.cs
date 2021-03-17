using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    [Table("Operators")]
    public class Operator : BaseEntity
    {
        [Required, MaxLength(150)]
        public string OperatorNameAr { get; set; }
        [Required, MaxLength(150)]
        public string OperatorNameEn { get; set; }
        [Required, MaxLength(150)]
        public int OperatorKey { get; set; }
    }
}
