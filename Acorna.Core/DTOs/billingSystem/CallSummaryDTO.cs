using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    public class CallSummaryDTO
    {
		public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string GroupNameAr { get; set; }
        public string GroupNameEn { get; set; }
        public string GroupName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal FreeSum { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal OfficialSum { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal PersonalSum { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal UnknownSum { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal FixedAmount { get; set; }

    }
}
