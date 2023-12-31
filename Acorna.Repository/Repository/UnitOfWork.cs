﻿using Acorna.Core.Entity;
using Acorna.Core.Entity.Security;
using Acorna.Core.Repository;
using Acorna.Core.Repository.Chat;
using Acorna.Core.Repository.Email;
using Acorna.Core.Repository.ICustomRepsitory;
using Acorna.Core.Repository.Notification;
using Acorna.Core.Repository.Project.BillingSystem.Report;
using Acorna.Repository.Repository.Chat;
using Acorna.Repository.Repository.CustomRepository;
using Acorna.Repository.Repository.Email;
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

        Lazy<IGovernorateRepository> LazyGovernorateRepository => new Lazy<IGovernorateRepository>(() => new GovernorateRepository(_dbFactory, _mapper));
        Lazy<IPhoneBookRepository> LazyPhoneBookRepository => new Lazy<IPhoneBookRepository>(() => new PhoneBookRepository(_dbFactory));
        Lazy<IIncomingNumbersRepository> LazyIncomingNumbersRepository => new Lazy<IIncomingNumbersRepository>(() => new IncomingNumbersRepository(_dbFactory));
        Lazy<IBillsSummaryRepository> LazyBillsSummaryRepository => new Lazy<IBillsSummaryRepository>(() => new BillsSummaryRepository(_dbFactory, _mapper, _userManager));
        Lazy<ICallDetailsReportRepository> LazyCallDetailsReportRepository => new Lazy<ICallDetailsReportRepository>(() => new CallDetailsViewRepository(_dbFactory));
        Lazy<IBillsDetailsRepository> LazyBillsDetailsRepository => new Lazy<IBillsDetailsRepository>(() => new BillsDetailsRepository(_dbFactory));
        Lazy<IGeneralSettingsRepository> LazyGeneralSettingsRepository => new Lazy<IGeneralSettingsRepository>(() => new GeneralSettingsRepository(_dbFactory));
        Lazy<IComingBillsRepository> LazyComingBillsRepository => new Lazy<IComingBillsRepository>(() => new ComingBillsRepository(_dbFactory, _mapper));
        Lazy<IEmailRepository> LazyEmailRepository => new Lazy<IEmailRepository>(() => new EmailRepository(_dbFactory));
        Lazy<IComingServicesRepository> LazyComingServicesRepository => new Lazy<IComingServicesRepository>(() => new ComingServicesRepository(_dbFactory));
        Lazy<IBillsRepository> LazyBillsRepository => new Lazy<IBillsRepository>(() => new BillRepository(_dbFactory, _mapper));

        public IGovernorateRepository GovernorateRepository => LazyGovernorateRepository.Value;
        public IPhoneBookRepository PhoneBookRepository => LazyPhoneBookRepository.Value;
        public IIncomingNumbersRepository IncomingNumbersRepository => LazyIncomingNumbersRepository.Value;
        public IBillsSummaryRepository BillsSummaryRepository => LazyBillsSummaryRepository.Value;
        public ICallDetailsReportRepository CallDetailsReportRepository => LazyCallDetailsReportRepository.Value;
        public IBillsDetailsRepository BillsDetailsRepository => LazyBillsDetailsRepository.Value;
        public IGeneralSettingsRepository GeneralSettingsRepository => LazyGeneralSettingsRepository.Value;
        public IComingBillsRepository ComingBillsRepository => LazyComingBillsRepository.Value;
        public IEmailRepository EmailRepository => LazyEmailRepository.Value;
        public IComingServicesRepository ComingServicesRepository => LazyComingServicesRepository.Value;
        public IBillsRepository BillsRepository => LazyBillsRepository.Value;

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
