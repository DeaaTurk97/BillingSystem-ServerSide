using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    [Table("Countries")]
    public class Country : BaseEntity
    {
        [Required, Column(TypeName = "nvarchar(100)")]
        public string CountryNameAr { get; set; }
        [Required, Column(TypeName = "nvarchar(100)")]
        public string CountryNameEn { get; set; }
        [Required, Column(TypeName = "int")]
        public int CountryKey { get; set; }
        [Required, Column(TypeName = "decimal(18, 3)")]
        public double PricePerMinute { get; set; }
    }
}
