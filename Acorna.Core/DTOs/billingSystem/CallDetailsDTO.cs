using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    public class CallDetailsDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public int GroupId { get; set; }
        public string GroupNameEn { get; set; }
        public string GroupNameAr { get; set; }
        public string GroupName { get; set; }
        public string DialledNumber { get; set; }
        public DateTime CallDateTime { get; set; }
        public string CallDuration { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal CallRetailPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal CallDiscountPrice { get; set; }
        public int TypePhoneNumberId { get; set; }
        public string TypePhoneNumberAr { get; set; }
        public string TypePhoneNumberEn { get; set; }
        public string TypePhoneNumberName { get; set; }
        public int ServiceUsedId { get; set; }
        public string ServiceUsedNameAr { get; set; }
        public string ServiceUsedNameEn { get; set; }
        public string ServiceUsedName { get; set; }
        public int TypeServiceUsedId { get; set; }
        public string TypeServiceUsedNameAr { get; set; }
        public string TypeServiceUsedNameEn { get; set; }
        public string TypeServiceUsedName { get; set; }
        public int BillId { get; set; }
        public int BillYear { get; set; }
        public int BillMonth { get; set; }
    }
}
