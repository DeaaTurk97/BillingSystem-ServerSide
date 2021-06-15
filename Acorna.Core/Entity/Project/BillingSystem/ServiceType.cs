using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    [Table("ServicesTypes")]
    public class ServiceType : BaseEntity
    {
        [Required, MaxLength(150)]
        public string ServiceNameAr { get; set; }
        [Required, MaxLength(150)]
        public string ServiceNameEn { get; set; }
        public bool IsCalculatedValue { get; set; }
    }
}
