using Acorna.Core.Entity;
using Acorna.Core.Repository.Chat;
using Acorna.Core.Repository.ICustomRepsitory;
using Acorna.Core.Repository.Notification;

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
        IBillsSummaryRepository BillsSummaryRepository { get; }

        bool SaveChanges();
    }
}
