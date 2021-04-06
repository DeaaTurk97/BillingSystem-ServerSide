﻿using Acorna.Core.Entity;
using Acorna.Core.Repository;
using Acorna.Core.Repository.ICustomRepsitory;
using Acorna.Repository.DataContext;
using Acorna.Repository.Repository.CustomRepository;
using System;
using System.Collections;

namespace Acorna.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AcornaDbContext _acornaDbContext;
        private IGovernorateRepository _governorateRepository;
        private Hashtable _repositories;

        public UnitOfWork(AcornaDbContext acornaDbContext)
        {
            _acornaDbContext = acornaDbContext;
        }

        public IGovernorateRepository GovernorateRepository => _governorateRepository = _governorateRepository ?? new GovernorateRepository(_acornaDbContext);

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _acornaDbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)_repositories[type];
        }

        public void SaveChanges()
        {
            _acornaDbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _acornaDbContext.Dispose();
            }
        }

    }
}
