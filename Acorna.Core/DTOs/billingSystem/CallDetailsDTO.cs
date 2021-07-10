using System;
using System.ComponentModel.DataAnnotations;
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
        public string DialledNumber { get; set; }
        public DateTime CallDateTime { get; set; }
        public string CallDuration { get; set; }
        public decimal CallRetailPrice { get; set; }
        public decimal CallDiscountPrice { get; set; }
        public int TypePhoneNumberId { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceTypeNameAr { get; set; }
        public string ServiceTypeNameEn { get; set; }
		public int BillId { get; set; }
		public int BillYear { get; set; }
        public int BillMonth { get; set; }
    }
}
