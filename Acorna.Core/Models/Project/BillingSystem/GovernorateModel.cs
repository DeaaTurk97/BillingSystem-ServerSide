namespace Acorna.Core.Models.Project.BillingSystem
{
    public class GovernorateModel : BaseModel
    {
        public string GovernorateNameAr { get; set; }
        public string GovernorateNameEn { get; set; }
        public int CountryId { get; set; }
        public string? CountryNameAr { get; set; }
        public string? CountryNameEn { get; set; }
    }
}
