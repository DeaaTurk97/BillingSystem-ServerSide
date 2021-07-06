using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    public class CallSummaryDTO
    {
		public int Id { get; set; }
		public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string GroupNameAr { get; set; }
        public string GroupNameEn { get; set; }
        public decimal FreeSum { get; set; }
        public decimal OfficialSum { get; set; }
        public decimal PersonalSum { get; set; }
        public decimal UnknownSum { get; set; }

    }
}
