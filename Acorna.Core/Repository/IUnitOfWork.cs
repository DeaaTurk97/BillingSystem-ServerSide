using Acorna.Core.Entity;

namespace Acorna.Core.Repository
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        void SaveChanges();
    }
}
