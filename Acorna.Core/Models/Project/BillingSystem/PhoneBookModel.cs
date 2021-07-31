namespace Acorna.Core.Models.Project.BillingSystem
{
    public class PhoneBookModel : BaseModel
    {
        public string PhoneNumber { get; set; }
        public string PhoneName { get; set; }
        public int PersonalUserId { get; set; }
        public string? UserName { get; set; }
        public int TypePhoneNumberId { get; set; }
        public string TypePhoneNumberName { get; set; }
        public int StatusNumberId { get; set; }
        public string StatusNumberName { get; set; }
        public int StatusAdminId { get; set; }
        public string GroupNameAr { get; set; }
        public string GroupNameEn { get; set; }
        public int? BillId { get; set; }
    }
}
