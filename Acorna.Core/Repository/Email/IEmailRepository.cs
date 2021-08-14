using System.Threading.Tasks;

namespace Acorna.Core.Repository.Email
{
    public interface IEmailRepository
    {
        Task<bool> ConfirmationEmail(string ToEmail, string confirmationLink);
        Task<bool> ImportBillEmail(string ToEmail);
        Task<bool> ReminderIdentifyNewNumbersEmail(string ToEmail);
        Task<bool> RejectNumberEmail(string ToEmail);
        Task<bool> PaidEmail(string ToEmail);
        Task<bool> UnpaidEmail(string ToEmail);
        Task<bool> SubmittedBillEmail(string ToEmail);
        Task<bool> ReminderTotalDueEmail(string ToEmail);
        Task<bool> ReminderStartPeriodSubmittBillEmail(string ToEmail);
        Task<bool> ReminderEndPeriodSubmittBillEmail(string ToEmail);
        Task<bool> ResetPasswordEmail(string ToEmail, string confirmationLink);
        Task<bool> SendEmailForgotPasswordAsync(string toEmails, string emailSubject, string emailBody);

        Task<bool> SendTestEmail();
    }
}
