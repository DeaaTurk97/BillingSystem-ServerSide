namespace Acorna.Core.DTOs.billingSystem
{
    public class BillsSummaryDTO : BaseDTO
    {
        public string BillMonth { get; set; }
        public string BillYear { get; set; }
        public bool IsPaid { get; set; }
        public string? BillNote { get; set; }
        public string? BillStatus { get; set; }
    }
}