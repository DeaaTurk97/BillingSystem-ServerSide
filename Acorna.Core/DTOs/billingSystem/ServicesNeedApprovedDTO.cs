using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.DTOs.billingSystem
{
    public class ServicesNeedApprovedDTO : BaseDTO
    {
        public string DialledNumber { get; set; }
        public int TypePhoneNumberId { get; set; }
        public string TypePhoneNumberName { get; set; }
        public string PhoneName { get; set; }
        public int StatusNumberId { get; set; }
        public DateTime CallDateTime { get; set; }
        public string CallDuration { get; set; }
        public decimal CallDiscountPrice { get; set; }
        public decimal CallRetailPrice { get; set; }
        public int StatusServiceUsedId { get; set; }
        public string ServiceUsedName { get; set; }
        public int TypeServiceUsedId { get; set; }
        public string TypeServiceUsedName { get; set; }
        public string StatusNumberName { get; set; }
    }
}
