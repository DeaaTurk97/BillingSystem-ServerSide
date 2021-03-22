using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Core.Sheard;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Acorna.Service.Project.BillingSystem
{
    public class OperatorService : IOperatorService
    {
        private readonly IRepository<Operator> _operatorRepository;
        private readonly IMapper _mapper;

        public OperatorService(IRepository<Operator> operatorRepository, IMapper mapper)
        {
            _operatorRepository = operatorRepository;
            _mapper = mapper;
        }

        public async Task<List<OperatorModel>> GetAllOperators()
        {
            try
            {
                List<Operator> operators = await _operatorRepository.GetAllAsync();
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
                PaginationRecord<Operator> operators = await _operatorRepository.GetAllAsync(pageIndex, pageSize, x => x.Id, OrderBy.Descending);
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
                Operator operators = await _operatorRepository.GetSingleAsync(groupId);
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
                return _operatorRepository.GetTotalCount();
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
                    _operatorRepository.Insert(operators);

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
                Operator operators = _operatorRepository.GetSingle(operatorModel.Id);
                operators.OperatorNameAr = operatorModel.OperatorNameAr;
                operators.OperatorNameEn = operatorModel.OperatorNameEn;
                operators.OperatorKey = operatorModel.OperatorKey;

                if (operators != null)
                    _operatorRepository.Update(operators);

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
                Operator operators = _operatorRepository.GetSingle(id);

                if (operators != null)
                    _operatorRepository.Delete(operators);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
