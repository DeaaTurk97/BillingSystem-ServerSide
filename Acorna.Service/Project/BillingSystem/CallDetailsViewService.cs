using Acorna.CommonMember;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem.Report;
using Acorna.Core.Repository;
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
			int countRecord = 0;
			var list = _unitOfWork.CallDetailsReportRepository.GetCallDetails(filter, out countRecord);
			PaginationRecord<CallDetailsDTO> paginationRecord = new PaginationRecord<CallDetailsDTO>
			{
				DataRecord = list,
				CountRecord = countRecord
			};
			return paginationRecord;
		}

		public PaginationRecord<CallSummaryDTO> GetCallSummary(CallsInfoFilterModel filter)
		{
			int countRecord = 0;
			var list = _unitOfWork.CallDetailsReportRepository.GetCallSummary(filter, out countRecord);
			PaginationRecord<CallSummaryDTO> paginationRecord = new PaginationRecord<CallSummaryDTO>
			{
				DataRecord = list,
				CountRecord = countRecord
			};
			return paginationRecord;
		}

		public PaginationRecord<CallFinanceDTO> GetCallFinance(CallsInfoFilterModel filter)
		{
			int countRecord = 0;
			var list = _unitOfWork.CallDetailsReportRepository.GetCallFinance(filter, out countRecord);
			PaginationRecord<CallFinanceDTO> paginationRecord = new PaginationRecord<CallFinanceDTO>
			{
				DataRecord = list,
				CountRecord = countRecord
			};
			return paginationRecord;
		}

	}
}
