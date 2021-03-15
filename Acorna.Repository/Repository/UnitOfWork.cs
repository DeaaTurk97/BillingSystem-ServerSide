using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Acorna.Core;
using Acorna.Core.Repository;

namespace Acorna.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public void BeginTransaction()
        {
            _dbFactory.GetDataContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbFactory.GetDataContext.Database.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            _dbFactory.GetDataContext.Database.RollbackTransaction();
        }

        public void SaveChanges()
        {
            _dbFactory.GetDataContext.SaveChanges();
        }
    }
}
