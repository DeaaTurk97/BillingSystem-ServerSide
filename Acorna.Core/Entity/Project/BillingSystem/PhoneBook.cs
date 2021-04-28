using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    [Table("PhonesBook")]
    public class PhoneBook : BaseEntity
    {
        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; }
        [Required, MaxLength(150)]
        public string PhoneName { get; set; }
        [Required, MaxLength(10)]
        public int? PersonalUserId { get; set; }
        [Required, MaxLength(10)]
        public int TypePhoneNumber { get; set; }
        [Required, MaxLength(10)]
        public int StatusNumberId { get; set; }
        [Required, MaxLength(10)]
        public int StatusAdminId { get; set; }
    }
}
