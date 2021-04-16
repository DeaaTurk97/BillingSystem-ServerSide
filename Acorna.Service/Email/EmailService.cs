using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.Models.Email;
using Acorna.Core.Repository;
using Acorna.Core.Services.Email;
using AutoMapper;
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
    }
}
