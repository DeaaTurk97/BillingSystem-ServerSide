using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.DTOs.billingSystem
{
    internal class HistoryDTO : BaseDTO
    {
        public string OldUserName { get; set; }
        public string NewUserName { get; set; }
        public int PhoneNumber { get; set; }
    }
}
