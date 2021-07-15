namespace Acorna.Core.Models.Project.BillingSystem
{
    public class BillsSummaryModel : BaseModel
    {
        public string BillMonth { get; set; }
        public string BillYear { get; set; }
        public bool IsPaid { get; set; }
        public string? BillNote { get; set; }
        public string? BillStatus { get; set; }
        public int UserId { get; set; }
    }
}
