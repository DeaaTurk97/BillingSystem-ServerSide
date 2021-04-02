namespace Acorna.Core.Models.Project.BillingSystem
{
    public class CountryModel : BaseModel
    {
        public string CountryNameAr { get; set; }
        public string CountryNameEn { get; set; }
        public int CountryKey { get; set; }
        public double PricePerMinute { get; set; }
    }
}
