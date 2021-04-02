using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Core.Sheard;
using Acorna.Repository.DataContext;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acorna.Service.Project.BillingSystem
{
    public class GovernorateService : IGovernorateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AcornaDbContext _acornaDbContext;

        public GovernorateService(IUnitOfWork unitOfWork, IMapper mapper, AcornaDbContext acornaDbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _acornaDbContext = acornaDbContext;
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
                IEnumerable<GovernorateModel> governorates = await (from gover in _acornaDbContext.Governorate
                                                             join cou in _acornaDbContext.Country on
                                                             gover.CountryId equals cou.Id
                                                             select new GovernorateModel
                                                             {
                                                                 Id = gover.Id,
                                                                 GovernorateNameAr = gover.GovernorateNameAr,
                                                                 GovernorateNameEn = gover.GovernorateNameEn,
                                                                 CountryNameAr = cou.CountryNameAr,
                                                                 CountryNameEn = cou.CountryNameEn,
                                                                 CountryId = cou.Id
                                                             }).OrderByDescending(s => s.Id)
                                                                     .Skip(pageSize * (pageIndex - 1))
                                                                     .Take(pageSize)
                                                                     .ToListAsync();

                PaginationRecord<GovernorateModel> paginationRecordModel = new PaginationRecord<GovernorateModel>
                {
                    DataRecord = governorates,
                    CountRecord = GetCountRecord(),
                };

                return paginationRecordModel;
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
                Governorate governorate = await _unitOfWork.GetRepository<Governorate>().GetSingleAsync(countryId);
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
