using Acorna.Core.Entity;
using Acorna.Core.Entity.Security;
using Acorna.Core.Repository;
using Acorna.Core.Repository.Chat;
using Acorna.Core.Repository.ICustomRepsitory;
using Acorna.Core.Repository.Notification;
using Acorna.Repository.Repository.Chat;
using Acorna.Repository.Repository.CustomRepository;
using Acorna.Repository.Repository.Notification;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;

namespace Acorna.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly IDbFactory _dbFactory;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UnitOfWork(IDbFactory dbFactory, IMapper mapper,
                         UserManager<User> userManager, IConfiguration configuration,
                         SignInManager<User> signInManager)
        {
            _dbFactory = dbFactory;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        Lazy<IGovernorateRepository> LazyGovernorateRepository => new Lazy<IGovernorateRepository>(() => new GovernorateRepository(_dbFactory));
        Lazy<IPhoneBookRepository> LazyPhoneBookRepository => new Lazy<IPhoneBookRepository>(() => new PhoneBookRepository(_dbFactory));
        Lazy<IIncomingNumbersRepository> LazyIncomingNumbersRepository => new Lazy<IIncomingNumbersRepository>(() => new IncomingNumbersRepository(_dbFactory));

        public IGovernorateRepository GovernorateRepository => LazyGovernorateRepository.Value;
        public IPhoneBookRepository PhoneBookRepository => LazyPhoneBookRepository.Value;
        public IIncomingNumbersRepository IncomingNumbersRepository => LazyIncomingNumbersRepository.Value;

        //base
        Lazy<IChatRepository> LazyChatRepository => new Lazy<IChatRepository>(() => new ChatRepository(_dbFactory));
        Lazy<ISecurityRepository> LazySecurityRepository => new Lazy<ISecurityRepository>(() =>
                    new SecurityRepository(_dbFactory, _mapper, _userManager, _signInManager, _configuration));
        Lazy<INotificationRepository> LazyNotificationRepository => new Lazy<INotificationRepository>(() => new NotificationRepository(_dbFactory, _mapper));
        //base
        public IChatRepository ChatRepository => LazyChatRepository.Value;
        public ISecurityRepository SecurityRepository => LazySecurityRepository.Value;
        public INotificationRepository NotificationRepository => LazyNotificationRepository.Value;

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbFactory);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)_repositories[type];
        }

        public bool SaveChanges()
        {
            bool returnValue = true;
            using (var dbContextTransaction = _dbFactory.DataContext.Database.BeginTransaction())
            {
                try
                {
                    _dbFactory.DataContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    //Log Exception Handling message                      
                    returnValue = false;
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }

            return returnValue;
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
                _dbFactory.DataContext.Dispose();
            }
        }

    }
}
