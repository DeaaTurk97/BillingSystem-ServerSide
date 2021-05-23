using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Core.Sheard;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Service.Project.BillingSystem
{
    internal class OperatorService : IOperatorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        internal OperatorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<OperatorModel>> GetAllOperators()
        {
            try
            {
                List<Operator> operators = await _unitOfWork.GetRepository<Operator>().GetAllAsync();
                return _mapper.Map<List<OperatorModel>>(operators); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PaginationRecord<OperatorModel>> GetOperators(int pageIndex, int pageSize)
        {
            try
            {
                PaginationRecord<Operator> operators = await _unitOfWork.GetRepository<Operator>().GetAllAsync(pageIndex, pageSize, x => x.Id, OrderBy.Descending);
                PaginationRecord<OperatorModel> paginationRecordModel = new PaginationRecord<OperatorModel>
                {
                    DataRecord = _mapper.Map<IEnumerable<OperatorModel>>(operators.DataRecord),
                    CountRecord = operators.CountRecord,
                };
                return paginationRecordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OperatorModel> GetOperatorId(int groupId)
        {
            try
            {
                Operator operators = await _unitOfWork.GetRepository<Operator>().GetSingleAsync(groupId);
                return _mapper.Map<OperatorModel>(operators);
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
                return _unitOfWork.GetRepository<Operator>().GetTotalCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddOperator(OperatorModel operatorModel)
        {
            try
            {
                Operator operators = _mapper.Map<Operator>(operatorModel);

                if (operators != null)
                {
                    _unitOfWork.GetRepository<Operator>().Insert(operators);
                    _unitOfWork.SaveChanges();
                }


                return operators.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateOperator(OperatorModel operatorModel)
        {
            try
            {
                Operator operators = _unitOfWork.GetRepository<Operator>().GetSingle(operatorModel.Id);

                if (operators != null)
                {
                    operators.OperatorNameAr = operatorModel.OperatorNameAr;
                    operators.OperatorNameEn = operatorModel.OperatorNameEn;
                    operators.OperatorKey = operatorModel.OperatorKey;

                    _unitOfWork.GetRepository<Operator>().Update(operators);
                    _unitOfWork.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteOperator(int id)
        {
            try
            {
                Operator operators = _unitOfWork.GetRepository<Operator>().GetSingle(id);

                if (operators != null)
                {
                    _unitOfWork.GetRepository<Operator>().Delete(operators);
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
