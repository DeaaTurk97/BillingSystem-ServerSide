using Acorna.CommonMember;
using Acorna.Core.DTOs;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Notification;
using Acorna.Core.Models.Project.BillingSystem.Report;
using Acorna.Core.Repository;
using Acorna.Core.Services;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Core.Sheard;
using AutoMapper;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Acorna.Core.DTOs.SystemEnum;


namespace Acorna.Service.Project.BillingSystem
{
	public class CallDetailsViewService : ICallDetailsViewService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		internal CallDetailsViewService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public PaginationRecord<CallDetailsDTO> GetCallDetails(CallsInfoFilterModel filter)
		{
			List<Bill> userNotifications = new List<Bill>();
			int countRecord = 0;
			SetReportLanguage(filter);
			var list = _unitOfWork.CallDetailsReportRepository.GetCallDetails(filter, out countRecord);
			PaginationRecord<CallDetailsDTO> paginationRecord = new PaginationRecord<CallDetailsDTO>
			{
				DataRecord = list,
				CountRecord = countRecord
			};

            //foreach (var callDetail in list)
            //{
            //    var totalPrice = callDetail.CallRetailPrice + callDetail.CallDiscountPrice;

            //    if (totalPrice > 0)
            //    {

            //        if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
            //        {
            //            userNotifications.ForEach(info =>
            //            {
            //                _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
            //                {
            //                    MessageText = "BillPriceGraterThanServicePrice",
            //                    IsRead = false,
            //                    Deleted = false,
            //                    RecipientId = info.UserId,
            //                    NotificationTypeId = (int)SystemEnum.NotificationType.BillPaid,
            //                    RecipientRoleId = 0,
            //                    ReferenceMassageId = info.Id
            //                });
            //            });
            //        }

            //        if (_unitOfWork.GeneralSettingsRepository.IsReminderByEmail())
            //        {

            //            _unitOfWork.EmailRepository.ServicePriceGraterThanServicePlan(_unitOfWork.SecurityRepository.GetEmailByUserId(Convert.ToInt32(callDetail.UserId)).Result);

            //        }
            //    }

            //}

            return paginationRecord;
		}

		public PaginationRecord<CallSummaryDTO> GetCallSummary(CallsInfoFilterModel filter)
		{
			int countRecord = 0;
			SetReportLanguage(filter);
			var list = _unitOfWork.CallDetailsReportRepository.GetCallSummary(filter, out countRecord);
			PaginationRecord<CallSummaryDTO> paginationRecord = new PaginationRecord<CallSummaryDTO>
			{
				DataRecord = list,
				CountRecord = countRecord,

			};
			return paginationRecord;
		}

		public PaginationRecord<CallFinanceDTO> GetCallFinance(CallsInfoFilterModel filter)
		{
			int countRecord = 0;
			SetReportLanguage(filter);
			var list = _unitOfWork.CallDetailsReportRepository.GetCallFinance(filter, out countRecord);
			PaginationRecord<CallFinanceDTO> paginationRecord = new PaginationRecord<CallFinanceDTO>
			{
				DataRecord = list,
				CountRecord = countRecord
			};
			return paginationRecord;
		}

		private void SetReportLanguage(CallsInfoFilterModel filter)
		{
			if (String.IsNullOrEmpty(filter.Lang))
			{
				var languageModel = _unitOfWork.SecurityRepository.GetLanguageInformations(filter.CurrentUserId);
				filter.Lang = languageModel.Result.LanguageCode.ToLower();
			}
		}

	}
}
