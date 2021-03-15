using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.IServices.SystemDefinition;
using Acorna.Core.Models.SystemDefinition;
using Acorna.Core.Repository;
using Acorna.Core.Sheard;

namespace Acorna.Service.SystemDefinition
{
    public class LanguageService : ILanguageService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Language> _languageRepository;

        public LanguageService(IMapper mapper, IRepository<Language> languageRepository)
        {
            _mapper = mapper;
            _languageRepository = languageRepository;
        }

        public List<LanguageModel> GetAllLanguages()
        {
            try
            {
                List<Language> languages = _languageRepository.GetAll();
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
                List<Language> languages = await _languageRepository.GetAllAsync();
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
                PaginationRecord<Language> language = await _languageRepository.GetAllAsync(pageIndex, pageSize, x => x.Id, OrderBy.Descending);
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
                    _languageRepository.Insert(language);
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
                Language language = await _languageRepository.GetSingleAsync(languageModel.Id);
                language.LanguageCode = languageModel.LanguageCode;
                language.LanguageDefaultDisply = languageModel.LanguageDefaultDisply;
                language.LanguageDirection = languageModel.LanguageDirection;
                language.LanguageFlag = languageModel.LanguageFlag;

                if (language != null)
                {
                    _languageRepository.Update(language);
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
                Language language = _languageRepository.GetSingle(id);
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

                Language language = _languageRepository.GetSingle(id);

                if (language != null)
                    _languageRepository.Delete(language);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
