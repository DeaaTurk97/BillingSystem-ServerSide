using Acorna.Core.Entity;
using Acorna.Core.Entity.Project.BillingSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.Repository
{
    public interface IUnitOfWork
    {
        //IRepository<Group> GroupRepository { get; }
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        //Task SaveChangesAsync();
        void SaveChanges();
        //void BeginTransaction();
        //void RollBackTransaction();
        //void CommitTransaction();
        //void SaveChanges();
    }
}
