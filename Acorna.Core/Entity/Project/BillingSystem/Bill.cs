using Acorna.Core.Entity.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity.Project.BillingSystem
{
    [Table("Bills")]
    public class Bill : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [Required, Column(TypeName = "datetime")]
        public DateTime BillDate { get; set; }
        [Required, Column(TypeName = "bit")]
        public bool SubmittedByAdmin { get; set; }
        [Required, Column(TypeName = "bit")]
        public bool SubmittedByUser { get; set; }
        [Required, Column(TypeName = "bit")]
        public bool IsPaid { get; set; }
        [Required, Column(TypeName = "bit")]
        public bool IsTerminal { get; set; }
        [Required, Column(TypeName = "int")]
        public int PaymentTypeId { get; set; }
        public DateTime SubmittedDate { get; set; }
        [Required, Column(TypeName = "int")]
        public int StatusBillId { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Note { get; set; }
        public virtual ICollection<BillDetails> BillDetails { get; set; }
    }
}
