using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    public class CallFinanceDTO
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string GroupNameAr { get; set; }
        public string GroupNameEn { get; set; }
        public string GroupName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal FreeServicesSum { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal OfficialServicesSum { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal PersonalServicesSum { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal UnknownServicesSum { get; set; }
    }
}
