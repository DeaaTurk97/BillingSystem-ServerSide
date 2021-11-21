namespace Acorna.Core.Models.Security
{
    public class VerificationCodeModel
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
    }
}
