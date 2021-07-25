using Acorna.Core.IServices.Chat;
using Acorna.Core.IServices.Notification;
using Acorna.Core.IServices.Project;
using Acorna.Core.IServices.SystemDefinition;
using Acorna.Core.Services.Email;
using Acorna.Core.Services.Project.BillingSystem;

namespace Acorna.Core.Services
{
    public interface IUnitOfWorkService
    {
        public IGeneralSettingsService GeneralSettingsService { get; }
        public IChatService ChatService { get; }
        public INotificationService NotificationService { get; }
        public ISecurityService SecurityService { get; }
        public ILanguageService LanguageService { get; }
        public IJobService JobService { get; }
        public ICountryService CountryService { get; }
        public IGovernorateService GovernorateService { get; }
        public IGroupService GroupService { get; }
        public IComingNumbersService IncomingNumbersService { get; }
        public IOperatorService OperatorService { get; }
        public IPhoneBookService PhoneBookService { get; }
        public IEmailService EmailService { get; }
        public IBillService BillService { get; }
        public ICallDetailsViewService CallDetailsViewService { get; }
        public IServiceType ServiceTypeService { get; }
        public IBillsSummaryService BillsSummaryService { get; }
        public ITypePhoneNumberService TypePhoneNumberService { get; }
        public IBillsDetailsService BillsDetailsService { get; }
    }
}
