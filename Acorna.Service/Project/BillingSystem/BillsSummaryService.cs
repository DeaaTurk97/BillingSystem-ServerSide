using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Core.Sheard;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acorna.Service.Project.BillingSystem
{
    public class BillsSummaryService : IBillsSummaryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BillsSummaryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationRecord<BillsSummaryModel>> GetBillsSummary(int pageIndex, int pageSize, int currentUserId)
        {
            try
            {
                IEnumerable<BillsSummaryDTO> billsSummaryDTO = await _unitOfWork.BillsSummaryRepository.GetBillsSummary(pageIndex, pageSize, currentUserId);

                PaginationRecord<BillsSummaryModel> paginationRecord = new PaginationRecord<BillsSummaryModel>
                {
                    DataRecord = _mapper.Map<IEnumerable<BillsSummaryModel>>(billsSummaryDTO),
                    CountRecord = billsSummaryDTO.Count()
                };
                return paginationRecord;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdatePayBill(int billId, bool isPaid)
        {
            try
            {
                Bill bill = _unitOfWork.GetRepository<Bill>().GetSingle(billId);

                if (bill != null)
                {
                    bill.IsPaid = isPaid;

                    _unitOfWork.GetRepository<Bill>().Update(bill);
                    _unitOfWork.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
