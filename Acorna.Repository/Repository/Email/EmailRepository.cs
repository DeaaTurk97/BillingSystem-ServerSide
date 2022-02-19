using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.Models.Email;
using Acorna.Core.Repository.Email;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Acorna.Repository.Repository.Email
{
    internal class EmailRepository : Repository<GeneralSetting>, IEmailRepository
    {
        private readonly IDbFactory _dbFactory;

        public EmailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<GeneralSetting> GeneralSettings
        {
            get
            {
                return _dbFactory.DataContext.GeneralSetting.ToList();
            }
        }

        public async Task<bool> ConfigureEmail(EmailModel emailModel)
        {
            try
            {
                NetworkCredential networkCredential = new NetworkCredential();
                MailMessage mailMessage = new MailMessage();

                emailModel.DisplayNameEmail = GeneralSettings.Find(x => x.SettingName.Trim() == "DisplayNameEmail")?.SettingValue;
                emailModel.HostServer = GeneralSettings.Find(x => x.SettingName.Trim() == "SMTPServer")?.SettingValue;
                emailModel.EnableSsl = Convert.ToBoolean(GeneralSettings.Find(x => x.SettingName.Trim() == "IsUseSSL")?.SettingValue);
                emailModel.UserEmail = GeneralSettings.Find(x => x.SettingName.Trim() == "SMTPUserEmail")?.SettingValue;
                emailModel.UserPassword = GeneralSettings.Find(x => x.SettingName.Trim() == "SMTPUserPassword")?.SettingValue;
                emailModel.IsRequiresAuthentication = Convert.ToBoolean(GeneralSettings.Find(x => x.SettingName.Trim() == "IsRequiresAuthentication")?.SettingValue);
                emailModel.Port = Convert.ToInt32(GeneralSettings.Find(x => x.SettingName.Trim() == "SMTPPortNo")?.SettingValue);


                mailMessage.From = new MailAddress(emailModel.UserEmail, emailModel.DisplayNameEmail);
                mailMessage.To.Add(emailModel.To);
                if (!string.IsNullOrEmpty(emailModel.CC))
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
                List<string> li = new List<string>();

                EmailModel emailModel = new EmailModel()
                {
                    To = GeneralSettings.Find(x => x.SettingName.Trim() == "EmailForTest")?.SettingValue,
                    Subject = GeneralSettings.Find(x => x.SettingName.Trim() == "EmailTestSubject")?.SettingValue,
                    CC = GeneralSettings.Find(x => x.SettingName.Trim() == "EmailTestCC")?.SettingValue,
                    Body = GeneralSettings.Find(x => x.SettingName.Trim() == "EmailTestBody")?.SettingValue,
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
                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = GeneralSettings.Find(x => x.SettingName.Trim() == "ImportSubject")?.SettingValue,
                    CC = GeneralSettings.Find(x => x.SettingName.Trim() == "ImportCC")?.SettingValue,
                    Body = GeneralSettings.Find(x => x.SettingName.Trim() == "ImportBody")?.SettingValue,
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
                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = GeneralSettings.Find(x => x.SettingName.Trim() == "ReminderIdentifyNewNumbersSubject")?.SettingValue,
                    CC = GeneralSettings.Find(x => x.SettingName.Trim() == "ReminderIdentifyNewNumbersCC")?.SettingValue,
                    Body = GeneralSettings.Find(x => x.SettingName.Trim() == "ReminderIdentifyNewNumbersBody")?.SettingValue,
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
                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = GeneralSettings.Find(x => x.SettingName.Trim() == "RejectionNumberSubject")?.SettingValue,
                    CC = GeneralSettings.Find(x => x.SettingName.Trim() == "RejectionNumberCC")?.SettingValue,
                    Body = GeneralSettings.Find(x => x.SettingName.Trim() == "RejectionNumberBody")?.SettingValue,
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
                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = GeneralSettings.Find(x => x.SettingName.Trim() == "PaidSubject")?.SettingValue,
                    CC = GeneralSettings.Find(x => x.SettingName.Trim() == "PaidCC")?.SettingValue,
                    Body = GeneralSettings.Find(x => x.SettingName.Trim() == "PaidBody")?.SettingValue,
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
                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = GeneralSettings.Find(x => x.SettingName.Trim() == "UnPaidSubject")?.SettingValue,
                    CC = GeneralSettings.Find(x => x.SettingName.Trim() == "UnPaidCC")?.SettingValue,
                    Body = GeneralSettings.Find(x => x.SettingName.Trim() == "UnPaidBody")?.SettingValue,
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
                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = GeneralSettings.Find(x => x.SettingName.Trim() == "SubmittedSubject")?.SettingValue,
                    CC = GeneralSettings.Find(x => x.SettingName.Trim() == "SubmittedCC")?.SettingValue,
                    Body = GeneralSettings.Find(x => x.SettingName.Trim() == "SubmittedBody")?.SettingValue,
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
                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = GeneralSettings.Find(x => x.SettingName.Trim() == "ReminderTotalDueSubject")?.SettingValue,
                    CC = GeneralSettings.Find(x => x.SettingName.Trim() == "ReminderTotalDueCC")?.SettingValue,
                    Body = GeneralSettings.Find(x => x.SettingName.Trim() == "ReminderTotalDueBody")?.SettingValue,
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
                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = GeneralSettings.Find(x => x.SettingName.Trim() == "ReminderStartPeriodSubmittBillSubject")?.SettingValue,
                    CC = GeneralSettings.Find(x => x.SettingName.Trim() == "ReminderStartPeriodSubmittBillCC")?.SettingValue,
                    Body = GeneralSettings.Find(x => x.SettingName.Trim() == "ReminderStartPeriodSubmittBillBody")?.SettingValue,
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
                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = GeneralSettings.Find(x => x.SettingName.Trim() == "ReminderEndPeriodSubmittBillSubject")?.SettingValue,
                    CC = GeneralSettings.Find(x => x.SettingName.Trim() == "ReminderEndPeriodSubmittBillCC")?.SettingValue,
                    Body = GeneralSettings.Find(x => x.SettingName.Trim() == "ReminderEndPeriodSubmittBillBody")?.SettingValue,
                };

                return await ConfigureEmail(emailModel);
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
                EmailModel emailModel = new EmailModel()
                {
                    To = GeneralSettings.Find(x => x.SettingName.Trim() == "EmailForTest")?.SettingValue,
                    Subject = GeneralSettings.Find(x => x.SettingName.Trim() == "EmailTestSubject")?.SettingValue,
                    CC = GeneralSettings.Find(x => x.SettingName.Trim() == "EmailTestCC")?.SettingValue,
                    Body = GeneralSettings.Find(x => x.SettingName.Trim() == "EmailTestBody")?.SettingValue,
                };

                return await ConfigureEmail(emailModel);
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
                EmailModel emailModel = new EmailModel()
                {
                    To = GeneralSettings.Find(x => x.SettingName.Trim() == "EmailForTest")?.SettingValue,
                    Subject = GeneralSettings.Find(x => x.SettingName.Trim() == "EmailTestSubject")?.SettingValue,
                    CC = GeneralSettings.Find(x => x.SettingName.Trim() == "EmailTestCC")?.SettingValue,
                    Body = GeneralSettings.Find(x => x.SettingName.Trim() == "EmailTestBody")?.SettingValue,
                };

                return await ConfigureEmail(emailModel);
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
                EmailModel emailModel = new EmailModel()
                {
                    To = toEmails,
                    Subject = emailSubject,
                    Body = emailBody,
                };

                return await ConfigureEmail(emailModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> NewServiceAdded(string ToEmail)
        {
            try
            {
                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = "New service added",
                    CC = "",
                    Body = "You have new service added",
                };

                return await ConfigureEmail(emailModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ServiceRemoved(string ToEmail)
        {
            try
            {
                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = "service removed",
                    CC = "",
                    Body = "You have new service removed",
                };

                return await ConfigureEmail(emailModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ServicePriceGraterThanServicePlan(string ToEmail)
        {
            try
            {
                EmailModel emailModel = new EmailModel()
                {
                    To = ToEmail,
                    Subject = "Service Price Grater Than Service Plan",
                    CC = "",
                    Body = "You have service price grater than plan service",
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
