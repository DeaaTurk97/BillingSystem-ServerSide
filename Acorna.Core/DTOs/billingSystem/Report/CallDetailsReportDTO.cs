using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    public class CallDetailsReportDTO
    {
		public int UserId { get; set; }
		public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public int GroupId { get; set; }
        public string GroupNameEn { get; set; }
        public string GroupNameAr { get; set; }
        public int DialledNumber { get; set; }
        public DateTime CallDateTime { get; set; }
        public string CallDuration { get; set; }
        public decimal CallRetailPrice { get; set; }
        public decimal CallDiscountPrice { get; set; }
        public int TypePhoneNumberId { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceNameAr { get; set; }
        public string ServiceNameEn { get; set; }
    }
}
