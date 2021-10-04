namespace Acorna.Core.Models.Project.BillingSystem
{
    public class ServiceUsedModel : BaseModel
    {
        public string ServiceUsedNameAr { get; set; }
        public string ServiceUsedNameEn { get; set; }
        public decimal? ServicePrice { get; set; }
        public bool IsCalculatedValue { get; set; }
        public bool IsNeedApproved { get; set; }
    }
}
