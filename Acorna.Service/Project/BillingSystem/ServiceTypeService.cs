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
    internal class ServiceTypeService : IServiceType
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        internal ServiceTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ServiceTypeModel>> GetAllServicesTypes()
        {
            try
            {
                List<ServiceType> servicesTypes = await _unitOfWork.GetRepository<ServiceType>().GetAllAsync();
                return _mapper.Map<List<ServiceTypeModel>>(servicesTypes); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PaginationRecord<ServiceTypeModel>> GetServicesTypes(int pageIndex, int pageSize)
        {
            try
            {
                PaginationRecord<ServiceType> servicesTypes = await _unitOfWork.GetRepository<ServiceType>().GetAllAsync(pageIndex, pageSize, x => x.Id, OrderBy.Descending);
                PaginationRecord<ServiceTypeModel> paginationRecordModel = new PaginationRecord<ServiceTypeModel>
                {
                    DataRecord = _mapper.Map<IEnumerable<ServiceTypeModel>>(servicesTypes.DataRecord),
                    CountRecord = servicesTypes.CountRecord,
                };
                return paginationRecordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ServiceTypeModel> GetServiceTypeId(int serviceTypeId)
        {
            try
            {
                ServiceType serviceType = await _unitOfWork.GetRepository<ServiceType>().GetSingleAsync(serviceTypeId);
                return _mapper.Map<ServiceTypeModel>(serviceType);
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
                return _unitOfWork.GetRepository<ServiceType>().GetTotalCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddServiceType(ServiceTypeModel serviceTypeModel)
        {
            try
            {
                ServiceType serviceType = _mapper.Map<ServiceType>(serviceTypeModel);

                if (serviceType != null)
                {
                    _unitOfWork.GetRepository<ServiceType>().Insert(serviceType);
                    _unitOfWork.SaveChanges();
                }

                return serviceType.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateServiceType(ServiceTypeModel serviceTypeModel)
        {
            try
            {
                ServiceType serviceType = _unitOfWork.GetRepository<ServiceType>().GetSingle(serviceTypeModel.Id);

                if (serviceType != null)
                {
                    serviceType.ServiceTypeNameAr = serviceTypeModel.ServiceTypeNameAr;
                    serviceType.ServiceTypeNameEn = serviceTypeModel.ServiceTypeNameEn;
                    serviceType.IsCalculatedValue = serviceTypeModel.IsCalculatedValue;

                    _unitOfWork.GetRepository<ServiceType>().Update(serviceType);
                    _unitOfWork.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteServiceType(int id)
        {
            try
            {
                ServiceType serviceType = _unitOfWork.GetRepository<ServiceType>().GetSingle(id);

                if (serviceType != null)
                {
                    _unitOfWork.GetRepository<ServiceType>().Delete(serviceType);
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
