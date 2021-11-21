namespace Acorna.Core.Models.Security
{
    public class VerificationCodeResponseModel
    {
        public bool IsVerifiedOtp { get; set; }
        public string Email { get; set; }
        public string TokenCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
