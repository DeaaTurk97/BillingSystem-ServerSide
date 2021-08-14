namespace Acorna.Core.DTOs.billingSystem
{
    public class BillsSummaryDTO : BaseDTO
    {
        public string BillMonth { get; set; }
        public string BillYear { get; set; }
        public bool IsPaid { get; set; }
        public string? BillNote { get; set; }
        public string? BillStatus { get; set; }
        public int UserId { get; set; }
        public string userName { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public bool SubmittedByAdmin { get; set; }
    }
}