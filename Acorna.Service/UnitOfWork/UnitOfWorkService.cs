﻿using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.IServices.Chat;
using Acorna.Core.IServices.Notification;
using Acorna.Core.IServices.Project;
using Acorna.Core.IServices.SystemDefinition;
using Acorna.Core.Repository;
using Acorna.Core.Services;
using Acorna.Core.Services.Email;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Service.Chatting;
using Acorna.Service.Email;
using Acorna.Service.Notification;
using Acorna.Service.Project;
using Acorna.Service.Project.BillingSystem;
using Acorna.Service.SystemDefinition;
using AutoMapper;
using System;

namespace Acorna.Service.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UnitOfWorkService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        Lazy<IGeneralSettingsService> LazyGeneralSettingsService => new Lazy<IGeneralSettingsService>(new GeneralSettingsService(_unitOfWork, _mapper));
        Lazy<IJobService> LazyJobService => new Lazy<IJobService>(() => new JobService(_unitOfWork, _mapper));
        Lazy<ICountryService> LazyCountryService => new Lazy<ICountryService>(() => new CountryService(_unitOfWork, _mapper));
        Lazy<IGovernorateService> LazyGovernorateService => new Lazy<IGovernorateService>(() => new GovernorateService(_unitOfWork, _mapper));
        Lazy<IGroupService> LazyGroupService => new Lazy<IGroupService>(() => new GroupService(_unitOfWork, _mapper));
        Lazy<IComingNumbersService> LazyIncomingNumbersService => new Lazy<IComingNumbersService>(() => new ComingNumbersService(_unitOfWork, _mapper));
        Lazy<IOperatorService> LazyOperatorService => new Lazy<IOperatorService>(() => new OperatorService(_unitOfWork, _mapper));
        Lazy<IPhoneBookService> LazyPhoneBookService => new Lazy<IPhoneBookService>(() => new PhoneBookService(_unitOfWork, _mapper));
        Lazy<IEmailService> LazyEmailService => new Lazy<IEmailService>(() => new EmailService(_unitOfWork));
        Lazy<IBillService> LazyBillService => new Lazy<IBillService>(() => new BillService(_unitOfWork, _mapper));
        Lazy<IServiceType> LazyServiceTypeService => new Lazy<IServiceType>(() => new ServiceTypeService(_unitOfWork, _mapper));
        Lazy<IBillsSummaryService> LazyBillsSummaryService => new Lazy<IBillsSummaryService>(() => new BillsSummaryService(_unitOfWork, _mapper));
        Lazy<ITypePhoneNumberService> LazyTypePhoneNumberService => new Lazy<ITypePhoneNumberService>(() => new TypePhoneNumberService(_unitOfWork, _mapper));
        Lazy<ICallDetailsViewService> LazyCallDetailsReportService => new Lazy<ICallDetailsViewService>(() => new CallDetailsViewService(_unitOfWork, _mapper));
        Lazy<IBillsDetailsService> LazyBillsDetailsService => new Lazy<IBillsDetailsService>(() => new BillsDetailsService(_unitOfWork, _mapper));

        Lazy<IReportService> LazyReportService => new Lazy<IReportService>(() => new ReportService(_unitOfWork, _mapper));

        //base
        Lazy<ISecurityService> LazySecurityService => new Lazy<ISecurityService>(() => new SecurityService(_unitOfWork, _mapper));
        Lazy<IChatService> LazyChatService => new Lazy<IChatService>(() => new ChatService(_unitOfWork, _mapper));
        Lazy<ILanguageService> LazyLanguageService => new Lazy<ILanguageService>(() => new LanguageService(_unitOfWork, _mapper));
        Lazy<INotificationService> LazyNotificationService => new Lazy<INotificationService>(() => new NotificationService(_unitOfWork, _mapper));


        public IGeneralSettingsService GeneralSettingsService => LazyGeneralSettingsService.Value;
        public IJobService JobService => LazyJobService.Value;
        public ICountryService CountryService => LazyCountryService.Value;
        public IGovernorateService GovernorateService => LazyGovernorateService.Value;
        public IGroupService GroupService => LazyGroupService.Value;
        public IComingNumbersService IncomingNumbersService => LazyIncomingNumbersService.Value;
        public IOperatorService OperatorService => LazyOperatorService.Value;
        public IPhoneBookService PhoneBookService => LazyPhoneBookService.Value;
        public IEmailService EmailService => LazyEmailService.Value;
        public IBillService BillService => LazyBillService.Value;
        public ICallDetailsViewService CallDetailsViewService => LazyCallDetailsReportService.Value;
        public IServiceType ServiceTypeService => LazyServiceTypeService.Value;
        public IBillsSummaryService BillsSummaryService => LazyBillsSummaryService.Value;
        public ITypePhoneNumberService TypePhoneNumberService => LazyTypePhoneNumberService.Value;
        public IBillsDetailsService BillsDetailsService => LazyBillsDetailsService.Value;

        public IReportService ReportService => LazyReportService.Value;

        //base
        public ISecurityService SecurityService => LazySecurityService.Value;
        public IChatService ChatService => LazyChatService.Value;
        public ILanguageService LanguageService => LazyLanguageService.Value;
        public INotificationService NotificationService => LazyNotificationService.Value;

    }
}
