using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    [Table("ServicesTypes")]
    public class ServiceType : BaseEntity
    {
        [Required, MaxLength(150)]
        public string ServiceTypeNameAr { get; set; }
        [Required, MaxLength(150)]
        public string ServiceTypeNameEn { get; set; }
        public bool IsCalculatedValue { get; set; }
    }
}
