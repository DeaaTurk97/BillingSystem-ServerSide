using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.Models.Email;
using Acorna.Core.Repository;
using Acorna.Core.Services.Email;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Acorna.Service.Email
{
    internal class EmailService : IEmailService
    {
        private readonly IUnitOfWork _unitOfWork;

        internal EmailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> SendTestEmail()
        {
            try
            {
                return await _unitOfWork.EmailRepository.SendTestEmail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ImportBillEmail(string ToEmail)
        {
            try
            {
                return await _unitOfWork.EmailRepository.ImportBillEmail(ToEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ReminderIdentifyNewNumbersEmail(string ToEmail)
        {
            try
            {
                return await _unitOfWork.EmailRepository.ReminderIdentifyNewNumbersEmail(ToEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RejectNumberEmail(string ToEmail)
        {
            try
            {
                return await _unitOfWork.EmailRepository.RejectNumberEmail(ToEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> PaidEmail(string ToEmail)
        {
            try
            {
                return await _unitOfWork.EmailRepository.PaidEmail(ToEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ApprovedEmail(string ToEmail)
        {
            try
            {
                return await _unitOfWork.EmailRepository.ApprovedEmail(ToEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UnpaidEmail(string ToEmail)
        {
            try
            {
                return await _unitOfWork.EmailRepository.UnpaidEmail(ToEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SubmittedBillEmail(string ToEmail)
        {
            try
            {
                return await _unitOfWork.EmailRepository.SubmittedBillEmail(ToEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ReminderTotalDueEmail(string ToEmail)
        {
            try
            {
                return await _unitOfWork.EmailRepository.ReminderTotalDueEmail(ToEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ReminderStartPeriodSubmittBillEmail(string ToEmail)
        {
            try
            {
                return await _unitOfWork.EmailRepository.ReminderStartPeriodSubmittBillEmail(ToEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ReminderEndPeriodSubmittBillEmail(string ToEmail)
        {
            try
            {
                return await _unitOfWork.EmailRepository.ReminderEndPeriodSubmittBillEmail(ToEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ConfirmationEmail(string ToEmail, string confirmationLink)
        {
            try
            {
                return await _unitOfWork.EmailRepository.ConfirmationEmail(ToEmail, confirmationLink);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ResetPasswordEmail(string ToEmail, string confirmationLink)
        {
            try
            {
                return await _unitOfWork.EmailRepository.ResetPasswordEmail(ToEmail, confirmationLink);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SendEmailForgotPasswordAsync(string toEmails, string emailSubject, string emailBody)
        {
            try
            {
                return await _unitOfWork.EmailRepository.SendEmailForgotPasswordAsync(toEmails, emailSubject, emailBody);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
