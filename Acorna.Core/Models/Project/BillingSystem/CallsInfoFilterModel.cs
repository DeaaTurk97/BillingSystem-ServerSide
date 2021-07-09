using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Models.Project.BillingSystem.Report
{
    public class CallsInfoFilterModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
        public int? ServiceTypeId { get; set; }
        public int? CountryId { get; set; }
        public int? CountryIdExclude { get; set; }
        public int? PhoneTypeId { get; set; }
        public int? TypePhoneNumberId { get; set; }
        public bool? IsSubmitted { get; set; }
        public int? PageIndex { get; set; } = 0;
        public int? PageSize { get; set; } = 10;
    }
}
