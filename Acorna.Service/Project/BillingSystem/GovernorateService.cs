using Acorna.Core.DTOs.billingSystem;
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
    internal class GovernorateService : IGovernorateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        internal GovernorateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GovernorateModel>> GetAllGovernorates()
        {
            try
            {
                List<Governorate> governorates = await _unitOfWork.GetRepository<Governorate>().GetAllAsync();
                return _mapper.Map<List<GovernorateModel>>(governorates);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PaginationRecord<GovernorateModel>> GetGovernorates(int pageIndex, int pageSize)
        {
            try
            {
                IEnumerable<GovernorateDTO> governoratesDTO = await _unitOfWork.GovernorateRepository.GetGovernorates(pageIndex, pageSize);

                PaginationRecord<GovernorateModel> paginationRecord = new PaginationRecord<GovernorateModel>
                {
                    DataRecord = _mapper.Map<IEnumerable<GovernorateModel>>(governoratesDTO),
                    CountRecord = GetCountRecord()
                };
                return paginationRecord;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GovernorateModel> GetGovernorateId(int countryId)
        {
            try
            {
                Governorate governorate = await _unitOfWork.GetRepository<Governorate>().GetAllAsync(countryId);
                return _mapper.Map<GovernorateModel>(governorate);
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
                return _unitOfWork.GetRepository<Governorate>().GetTotalCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddGovernorate(GovernorateModel governorateModel)
        {
            try
            {
                Governorate governorate = _mapper.Map<Governorate>(governorateModel);

                if (governorate != null)
                {
                    _unitOfWork.GetRepository<Governorate>().Insert(governorate);
                    _unitOfWork.SaveChanges();
                }

                return governorate.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateGovernorate(GovernorateModel governorateModel)
        {
            try
            {
                Governorate governorate = _unitOfWork.GetRepository<Governorate>().GetSingle(governorateModel.Id);

                if (governorate != null)
                {
                    governorate.GovernorateNameAr = governorateModel.GovernorateNameAr;
                    governorate.GovernorateNameEn = governorateModel.GovernorateNameEn;
                    governorate.CountryId = governorateModel.CountryId;

                    _unitOfWork.GetRepository<Governorate>().Update(governorate);
                    _unitOfWork.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteGovernorate(int id)
        {
            try
            {
                Governorate governorate = _unitOfWork.GetRepository<Governorate>().GetSingle(id);

                if (governorate != null)
                {
                    _unitOfWork.GetRepository<Governorate>().Delete(governorate);
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
