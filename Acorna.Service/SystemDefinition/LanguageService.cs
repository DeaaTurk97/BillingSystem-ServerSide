using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.IServices.SystemDefinition;
using Acorna.Core.Models.SystemDefinition;
using Acorna.Core.Repository;
using Acorna.Core.Sheard;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Service.SystemDefinition
{
    internal class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        internal LanguageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<LanguageModel> GetAllLanguages()
        {
            try
            {
                List<Language> languages = _unitOfWork.GetRepository<Language>().GetAll();
                return _mapper.Map<List<LanguageModel>>(languages);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<LanguageModel>> GetAllLanguagesAsync()
        {
            try
            {
                List<Language> languages = await _unitOfWork.GetRepository<Language>().GetAllAsync();
                return _mapper.Map<List<LanguageModel>>(languages);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<PaginationRecord<LanguageModel>> GetAllLanguagesAsync(int pageIndex, int pageSize)
        {
            try
            {
                PaginationRecord<Language> language = await _unitOfWork.GetRepository<Language>().GetAllAsync(pageIndex, pageSize, x => x.Id, OrderBy.Descending);
                PaginationRecord<LanguageModel> paginationRecordModel = new PaginationRecord<LanguageModel>
                {
                    DataRecord = _mapper.Map<IEnumerable<LanguageModel>>(language.DataRecord),
                    CountRecord = language.CountRecord,
                };
                return paginationRecordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddLanguage(LanguageModel languageModel)
        {
            int id = 0;
            try
            {
                Language language = _mapper.Map<Language>(languageModel);
                if (language != null)
                    _unitOfWork.GetRepository<Language>().Insert(language);
                id = language.Id;
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateLanguageAsync(LanguageModel languageModel)
        {
            try
            {
                Language language = await _unitOfWork.GetRepository<Language>().GetSingleAsync(languageModel.Id);
                language.LanguageCode = languageModel.LanguageCode;
                language.LanguageDefaultDisply = languageModel.LanguageDefaultDisply;
                language.LanguageDirection = languageModel.LanguageDirection;
                language.LanguageFlag = languageModel.LanguageFlag;

                if (language != null)
                {
                    _unitOfWork.GetRepository<Language>().Update(language);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LanguageModel GetLanguageById(int id)
        {
            try
            {
                Language language = _unitOfWork.GetRepository<Language>().GetSingle(id);
                return _mapper.Map<LanguageModel>(language);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public bool DeleteLanguage(int id)
        {
            try
            {

                Language language = _unitOfWork.GetRepository<Language>().GetSingle(id);

                if (language != null)
                    _unitOfWork.GetRepository<Language>().Delete(language);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
