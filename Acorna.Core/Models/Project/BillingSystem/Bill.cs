﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.Models.Project.BillingSystem
{
    public class Bill : BaseModel
    {
        public int UserId { get; set; }
        public DateTime BillDate { get; set; }
        public bool SubmittedByAdmin { get; set; }
        public bool SubmittedByUser { get; set; }
        public bool IsPaid { get; set; }
        public bool IsTerminal { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string? Note { get; set; }
    }
}
