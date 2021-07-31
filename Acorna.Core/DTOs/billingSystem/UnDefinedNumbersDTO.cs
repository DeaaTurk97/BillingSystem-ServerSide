namespace Acorna.Core.DTOs.billingSystem
{
    public class UnDefinedNumbersDTO : BaseDTO
    {
        public string DialledNumber { get; set; }
        public int TypePhoneNumberId { get; set; }
        public string PhoneName { get; set; }
        public int StatusNumberId { get; set; }
        public string StatusNumberName { get; set; }
    }
}
