using Acorna.Core.Entity;
using Acorna.Core.Repository.Chat;
using Acorna.Core.Repository.Email;
using Acorna.Core.Repository.ICustomRepsitory;
using Acorna.Core.Repository.Notification;
using Acorna.Core.Repository.Project.BillingSystem.Report;

namespace Acorna.Core.Repository
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        IChatRepository ChatRepository { get; }
        ISecurityRepository SecurityRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IGovernorateRepository GovernorateRepository { get; }
        IPhoneBookRepository PhoneBookRepository { get; }
        IIncomingNumbersRepository IncomingNumbersRepository { get; }
        IComingBillsRepository ComingBillsRepository { get; }
        IBillsSummaryRepository BillsSummaryRepository { get; }
        ICallDetailsReportRepository CallDetailsReportRepository { get; }
        IBillsDetailsRepository BillsDetailsRepository { get; }
        IGeneralSettingsRepository GeneralSettingsRepository { get; }
        IEmailRepository EmailRepository { get; }

        bool SaveChanges();
    }
}
