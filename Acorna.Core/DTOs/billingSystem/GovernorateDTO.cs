namespace Acorna.Core.DTOs.billingSystem
{
    public class GovernorateDTO : BaseDTO
    {
        public string GovernorateNameAr { get; set; }
        public string GovernorateNameEn { get; set; }
        public int CountryId { get; set; }
        public string? CountryNameAr { get; set; }
        public string? CountryNameEn { get; set; }
    }
}
