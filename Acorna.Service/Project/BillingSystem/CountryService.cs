using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Core.Sheard;
using Acorna.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Service.Project.BillingSystem
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CountryModel>> GetAllCountries()
        {
            try
            {
                List<Country> countries = await _unitOfWork.GetRepository<Country>().GetAllAsync();
                return _mapper.Map<List<CountryModel>>(countries);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PaginationRecord<CountryModel>> GetCountries(int pageIndex, int pageSize)
        {
            try
            {
                PaginationRecord<Country> country = await _unitOfWork.GetRepository<Country>().GetAllAsync(pageIndex, pageSize, x => x.Id, OrderBy.Descending);
                PaginationRecord<CountryModel> paginationRecordModel = new PaginationRecord<CountryModel>
                {
                    DataRecord = _mapper.Map<IEnumerable<CountryModel>>(country.DataRecord),
                    CountRecord = country.CountRecord,
                };

                return paginationRecordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CountryModel> GetCountryId(int countryId)
        {
            try
            {
                Country country = await _unitOfWork.GetRepository<Country>().GetSingleAsync(countryId);
                return _mapper.Map<CountryModel>(country);
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
                return _unitOfWork.GetRepository<Country>().GetTotalCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddCountry(CountryModel countryModel)
        {
            try
            {
                Country country = _mapper.Map<Country>(countryModel);

                if (country != null)
                {
                    _unitOfWork.GetRepository<Country>().Insert(country);
                    _unitOfWork.SaveChanges();
                }

                return country.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateCountry(CountryModel countryModel)
        {
            try
            {
                Country country = _unitOfWork.GetRepository<Country>().GetSingle(countryModel.Id);

                if (country != null)
                {
                    country.CountryNameAr = countryModel.CountryNameAr;
                    country.CountryNameEn = countryModel.CountryNameEn;
                    country.CountryKey = countryModel.CountryKey;
                    country.PricePerMinute = countryModel.PricePerMinute;

                    _unitOfWork.GetRepository<Country>().Update(country);
                    _unitOfWork.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCountry(int id)
        {
            try
            {
                Country country = _unitOfWork.GetRepository<Country>().GetSingle(id);

                if (country != null)
                {
                    _unitOfWork.GetRepository<Country>().Delete(country);
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
