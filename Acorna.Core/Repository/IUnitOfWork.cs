using Acorna.Core.Entity;
using Acorna.Core.Repository.ICustomRepsitory;

namespace Acorna.Core.Repository
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        IGovernorateRepository GovernorateRepository { get; }

        void SaveChanges();
    }
}
