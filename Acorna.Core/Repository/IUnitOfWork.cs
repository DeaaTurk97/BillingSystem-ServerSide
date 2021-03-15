using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.Repository
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void RollBackTransaction();
        void CommitTransaction();
        void SaveChanges();
    }
}
