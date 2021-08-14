using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    public class BillDetails : BaseEntity
    {
        [Required, Column(TypeName = "int")]
        public int? PhoneBookId { get; set; }
        [Required, Column(TypeName = "datetime")]
        public DateTime CallDateTime { get; set; }
        [Required, Column(TypeName = "nvarchar(250)")]
        public string CallDuration { get; set; }
        [Required, Column(TypeName = "decimal(18, 3)")]
        public decimal CallNetPrice { get; set; }
        [Required, Column(TypeName = "decimal(18, 3)")]
        public decimal CallRetailPrice { get; set; }
        [Required, Column(TypeName = "decimal(18, 3)")]
        public decimal CallDiscountPrice { get; set; }
        [Required, Column(TypeName = "nvarchar(250)")]
        public string PhoneNumber { get; set; }
        [Required, Column(TypeName = "int")]
        public int TypePhoneNumberId { get; set; }
        [Required, Column(TypeName = "int")]
        public int ServiceTypeId { get; set; }
        public int OperatorId { get; set; }
        [Required, Column(TypeName = "nvarchar(50)")]
        public string DataUsage { get; set; }
        public int BillId { get; set; }
    }
}
