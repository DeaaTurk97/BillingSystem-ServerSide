using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Core.Sheard;
using Acorna.Repository.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorna.Service.Project.BillingSystem
{
    public class HistoryServices : IHistoryServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public HistoryServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<HistoryModel>> GetAllHistory()
        {
            try
            {
                List<History> history = await _unitOfWork.GetRepository<History>().GetAllAsync();
                List<HistoryModel> HistoryModels = _mapper.Map<List<HistoryModel>>(history);
                return HistoryModels;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PaginationRecord<HistoryModel>> GetHistory(int pageIndex, int pageSize)
        {
            try
            {
                PaginationRecord<History> history = await _unitOfWork.GetRepository<History>().GetAllAsync(pageIndex, pageSize, x => x.Id, null, OrderBy.Descending);
                PaginationRecord<HistoryModel> paginationRecordModel = new PaginationRecord<HistoryModel>
                {
                    DataRecord = _mapper.Map<IEnumerable<HistoryModel>>(history.DataRecord),
                    CountRecord = history.CountRecord,
                };
                return paginationRecordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HistoryModel> GetHistoryId(int historyId)
        {
            try
            {
                History history = await _unitOfWork.GetRepository<History>().GetSingleAsync(historyId);
                HistoryModel historyModel = _mapper.Map<HistoryModel>(history);
                return historyModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<HistoryModel>> GetHistoriesByPhoneNumber(string phoneNumber)
        {
            try
            {
                List<History> history = await _unitOfWork.GetRepository<History>().GetAllAsync();
              
                var historyModel = _mapper.Map<List<HistoryModel>>(history.Where(x => x.PhoneNumber.Equals(phoneNumber)));
                return historyModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetCountRecord()
        {
            try
            {
                return _unitOfWork.GetRepository<History>().GetTotalCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddHistory(HistoryModel historyModel)
        {
            try
            {
                History history = _mapper.Map<History>(historyModel);

                if (history != null)
                {
                    _unitOfWork.GetRepository<History>().Insert(history);

                    _unitOfWork.SaveChanges();
                }

                return history.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool UpdateHistory(HistoryModel historyModel)
        {
            History history = _unitOfWork.GetRepository<History>().GetSingle(historyModel.Id);

            if (history != null)
            {
                _unitOfWork.GetRepository<History>().Update(history);
                _unitOfWork.SaveChanges();
            }

            return true;
        }

        public bool DeleteHistory(int id)
        {
            try
            {
                History history = _unitOfWork.GetRepository<History>().GetSingle(id);
              
                if (history != null)
                {
                    _unitOfWork.GetRepository<History>().Delete(history);
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
