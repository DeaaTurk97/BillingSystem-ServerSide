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
    public class EmailService : IEmailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ConfigureEmail(EmailModel emailModel)
        {
            try
            {
                NetworkCredential networkCredential = new NetworkCredential();
                MailMessage mailMessage = new MailMessage();

                List<GeneralSetting> generalSettingModels = await _unitOfWork.GetRepository<GeneralSetting>().GetAllAsync();

                emailModel.DisplayNameEmail = generalSettingModels.Find(x => x.SettingName.Trim() == "DisplayNameEmail")?.SettingValue;
                emailModel.HostServer = generalSettingModels.Find(x => x.SettingName.Trim() == "SMTPServer")?.SettingValue;
                emailModel.EnableSsl = Convert.ToBoolean(generalSettingModels.Find(x => x.SettingName.Trim() == "IsUseSSL")?.SettingValue);
                emailModel.UserEmail = generalSettingModels.Find(x => x.SettingName.Trim() == "SMTPUserEmail")?.SettingValue;
                emailModel.UserPassword = generalSettingModels.Find(x => x.SettingName.Trim() == "SMTPUserPassword")?.SettingValue;
                emailModel.IsRequiresAuthentication = Convert.ToBoolean(generalSettingModels.Find(x => x.SettingName.Trim() == "IsRequiresAuthentication")?.SettingValue);
                emailModel.Port = Convert.ToInt32(generalSettingModels.Find(x => x.SettingName.Trim() == "SMTPPortNo")?.SettingValue);

                
                mailMessage.From = new MailAddress(emailModel.UserEmail, emailModel.DisplayNameEmail);
                mailMessage.To.Add(emailModel.To);
                if(!string.IsNullOrEmpty(emailModel.CC))
                {
                    mailMessage.CC.Add(emailModel.CC);
                }
                mailMessage.Subject = emailModel.Subject;
                mailMessage.Body = emailModel.Body;
                mailMessage.IsBodyHtml = false;

                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Host = emailModel.HostServer;
                    smtpClient.EnableSsl = emailModel.EnableSsl;

                    if (emailModel.IsRequiresAuthentication)
                    {
                        networkCredential = new NetworkCredential(emailModel.UserEmail, emailModel.UserPassword);
                    }
                    else
                    {
                        smtpClient.UseDefaultCredentials = false;
                    }

                    smtpClient.Credentials = networkCredential;
                    smtpClient.Port = emailModel.Port;
                    smtpClient.Send(mailMessage);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SendTestEmail()
        {
            try
            {
                List<GeneralSetting> generalSettingModels = await _unitOfWork.GetRepository<GeneralSetting>().GetAllAsync();

                EmailModel emailModel = new EmailModel()
                {
                    To = generalSettingModels.Find(x => x.SettingName.Trim() == "EmailForTest")?.SettingValue,
                    Subject = generalSettingModels.Find(x => x.SettingName.Trim() == "EmailTestSubject")?.SettingValue,
                    CC = generalSettingModels.Find(x => x.SettingName.Trim() == "EmailTestCC")?.SettingValue,
                    Body = generalSettingModels.Find(x => x.SettingName.Trim() == "EmailTestBody")?.SettingValue,
                };

                return await ConfigureEmail(emailModel);
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
                List<GeneralSetting> generalSettingModels = await _unitOfWork.GetRepository<GeneralSetting>().GetAllAsync();

                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = generalSettingModels.Find(x => x.SettingName.Trim() == "ImportSubject")?.SettingValue,
                    CC = generalSettingModels.Find(x => x.SettingName.Trim() == "ImportCC")?.SettingValue,
                    Body = generalSettingModels.Find(x => x.SettingName.Trim() == "ImportBody")?.SettingValue,
                };

                return await ConfigureEmail(emailModel);
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
                List<GeneralSetting> generalSettingModels = await _unitOfWork.GetRepository<GeneralSetting>().GetAllAsync();

                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = generalSettingModels.Find(x => x.SettingName.Trim() == "ReminderIdentifyNewNumbersSubject")?.SettingValue,
                    CC = generalSettingModels.Find(x => x.SettingName.Trim() == "ReminderIdentifyNewNumbersCC")?.SettingValue,
                    Body = generalSettingModels.Find(x => x.SettingName.Trim() == "ReminderIdentifyNewNumbersBody")?.SettingValue,
                };

                return await ConfigureEmail(emailModel);
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
                List<GeneralSetting> generalSettingModels = await _unitOfWork.GetRepository<GeneralSetting>().GetAllAsync();

                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = generalSettingModels.Find(x => x.SettingName.Trim() == "RejectionNumberSubject")?.SettingValue,
                    CC = generalSettingModels.Find(x => x.SettingName.Trim() == "RejectionNumberCC")?.SettingValue,
                    Body = generalSettingModels.Find(x => x.SettingName.Trim() == "RejectionNumberBody")?.SettingValue,
                };

                return await ConfigureEmail(emailModel);
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
                List<GeneralSetting> generalSettingModels = await _unitOfWork.GetRepository<GeneralSetting>().GetAllAsync();

                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = generalSettingModels.Find(x => x.SettingName.Trim() == "PaidSubject")?.SettingValue,
                    CC = generalSettingModels.Find(x => x.SettingName.Trim() == "PaidCC")?.SettingValue,
                    Body = generalSettingModels.Find(x => x.SettingName.Trim() == "PaidBody")?.SettingValue,
                };

                return await ConfigureEmail(emailModel);
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
                List<GeneralSetting> generalSettingModels = await _unitOfWork.GetRepository<GeneralSetting>().GetAllAsync();

                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = generalSettingModels.Find(x => x.SettingName.Trim() == "UnPaidSubject")?.SettingValue,
                    CC = generalSettingModels.Find(x => x.SettingName.Trim() == "UnPaidCC")?.SettingValue,
                    Body = generalSettingModels.Find(x => x.SettingName.Trim() == "UnPaidBody")?.SettingValue,
                };

                return await ConfigureEmail(emailModel);
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
                List<GeneralSetting> generalSettingModels = await _unitOfWork.GetRepository<GeneralSetting>().GetAllAsync();

                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = generalSettingModels.Find(x => x.SettingName.Trim() == "SubmittedSubject")?.SettingValue,
                    CC = generalSettingModels.Find(x => x.SettingName.Trim() == "SubmittedCC")?.SettingValue,
                    Body = generalSettingModels.Find(x => x.SettingName.Trim() == "SubmittedBody")?.SettingValue,
                };

                return await ConfigureEmail(emailModel);
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
                List<GeneralSetting> generalSettingModels = await _unitOfWork.GetRepository<GeneralSetting>().GetAllAsync();

                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = generalSettingModels.Find(x => x.SettingName.Trim() == "ReminderTotalDueSubject")?.SettingValue,
                    CC = generalSettingModels.Find(x => x.SettingName.Trim() == "ReminderTotalDueCC")?.SettingValue,
                    Body = generalSettingModels.Find(x => x.SettingName.Trim() == "ReminderTotalDueBody")?.SettingValue,
                };

                return await ConfigureEmail(emailModel);
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
                List<GeneralSetting> generalSettingModels = await _unitOfWork.GetRepository<GeneralSetting>().GetAllAsync();

                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = generalSettingModels.Find(x => x.SettingName.Trim() == "ReminderStartPeriodSubmittBillSubject")?.SettingValue,
                    CC = generalSettingModels.Find(x => x.SettingName.Trim() == "ReminderStartPeriodSubmittBillCC")?.SettingValue,
                    Body = generalSettingModels.Find(x => x.SettingName.Trim() == "ReminderStartPeriodSubmittBillBody")?.SettingValue,
                };

                return await ConfigureEmail(emailModel);
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
                List<GeneralSetting> generalSettingModels = await _unitOfWork.GetRepository<GeneralSetting>().GetAllAsync();

                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = generalSettingModels.Find(x => x.SettingName.Trim() == "ReminderEndPeriodSubmittBillSubject")?.SettingValue,
                    CC = generalSettingModels.Find(x => x.SettingName.Trim() == "ReminderEndPeriodSubmittBillCC")?.SettingValue,
                    Body = generalSettingModels.Find(x => x.SettingName.Trim() == "ReminderEndPeriodSubmittBillBody")?.SettingValue,
                };

                return await ConfigureEmail(emailModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
