using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.Models.Project.BillingSystem
{
    public class BillDetails : BaseModel
    {
        public int BillId { get; set; }
        public string PhoneBookId { get; set; }
        public DateTime CallDateTime { get; set; }
        public string CallDuration { get; set; }
        public decimal CallNetPrice { get; set; }
        public decimal CallRetailPrice { get; set; }
        public decimal CallDiscountPrice { get; set; }
        public string PhoneNumber { get; set; }
        public int TypePhoneNumberId { get; set; }
        public int ServiceTypeID { get; set; }
        public int OperatorId { get; set; }
        public string DataUsage { get; set; }
    }
}
