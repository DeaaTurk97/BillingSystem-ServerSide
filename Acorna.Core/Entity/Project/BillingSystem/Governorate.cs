using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    public class Governorate : BaseEntity
    {
        [Required, Column(TypeName = "nvarchar(100)")]
        public string GovernorateNameAr { get; set; }
        [Required, Column(TypeName = "nvarchar(100)")]
        public string GovernorateNameEn { get; set; }
        [ForeignKey("CountryId")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
