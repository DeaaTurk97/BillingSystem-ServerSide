using Acorna.CommonMember;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem.Report;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Core.Services.Project.BillingSystem.Report;
using AutoMapper;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Acorna.Core.DTOs.SystemEnum;


namespace Acorna.Service.Project.BillingSystem.Report
{
    public class CallDetailsReportService : ICallDetailsReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        internal CallDetailsReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<List<CallDetailsReportDTO>> GetReport(CallDetailsReportModel filter)
		{
           var list = _unitOfWork.CallDetailsReportRepository.GetReport(filter);
            return list;
        }

    }
}
