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
    internal class ServiceUsedService : IServiceUsed
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        internal ServiceUsedService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ServiceUsedModel>> GetAllServicesUsed()
        {
            try
            {
                List<ServiceUsed> servicesUsed = await _unitOfWork.GetRepository<ServiceUsed>().GetAllAsync();
                return _mapper.Map<List<ServiceUsedModel>>(servicesUsed); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PaginationRecord<ServiceUsedModel>> GetServicesUsed(int pageIndex, int pageSize)
        {
            try
            {
                PaginationRecord<ServiceUsed> servicesUsed = await _unitOfWork.GetRepository<ServiceUsed>().GetAllAsync(pageIndex, pageSize, x => x.Id, OrderBy.Descending);
                PaginationRecord<ServiceUsedModel> paginationRecordModel = new PaginationRecord<ServiceUsedModel>
                {
                    DataRecord = _mapper.Map<IEnumerable<ServiceUsedModel>>(servicesUsed.DataRecord),
                    CountRecord = servicesUsed.CountRecord,
                };
                return paginationRecordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ServiceUsedModel> GetServiceUsedId(int serviceTypeId)
        {
            try
            {
                ServiceUsed serviceUsed = await _unitOfWork.GetRepository<ServiceUsed>().GetSingleAsync(serviceTypeId);
                return _mapper.Map<ServiceUsedModel>(serviceUsed);
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
                return _unitOfWork.GetRepository<ServiceUsed>().GetTotalCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddServiceUsed(ServiceUsedModel serviceUsedModel)
        {
            try
            {
                ServiceUsed serviceUsed = _mapper.Map<ServiceUsed>(serviceUsedModel);

                if (serviceUsed != null)
                {
                    _unitOfWork.GetRepository<ServiceUsed>().Insert(serviceUsed);
                    _unitOfWork.SaveChanges();
                }

                return serviceUsed.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateServiceUsed(ServiceUsedModel serviceUsedModel)
        {
            try
            {
                ServiceUsed serviceUsed = _unitOfWork.GetRepository<ServiceUsed>().GetSingle(serviceUsedModel.Id);

                if (serviceUsed != null)
                {
                    serviceUsed.ServiceUsedNameAr = serviceUsedModel.ServiceUsedNameAr;
                    serviceUsed.ServiceUsedNameEn = serviceUsedModel.ServiceUsedNameEn;
                    serviceUsed.ServicePrice = serviceUsedModel.ServicePrice;
                    serviceUsed.IsCalculatedValue = serviceUsedModel.IsCalculatedValue;
                    serviceUsed.IsNeedApproved = serviceUsedModel.IsNeedApproved;
                    serviceUsed.NonOfficial = serviceUsedModel.NonOfficial;

                    _unitOfWork.GetRepository<ServiceUsed>().Update(serviceUsed);
                    _unitOfWork.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteServiceUsed(int id)
        {
            try
            {
                ServiceUsed serviceUsed = _unitOfWork.GetRepository<ServiceUsed>().GetSingle(id);

                if (serviceUsed != null)
                {
                    _unitOfWork.GetRepository<ServiceUsed>().Delete(serviceUsed);
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
