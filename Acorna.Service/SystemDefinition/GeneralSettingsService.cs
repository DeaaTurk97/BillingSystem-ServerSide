using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.IServices.Project;
using Acorna.Core.Models.SystemDefinition;
using Acorna.Core.Repository;

namespace Acorna.Service.SystemDefinition
{
    public class GeneralSettingsService : IGeneralSettingsService
    {
        private readonly IRepository<GeneralSetting> _repository;
        private readonly IMapper _mapper;

        public GeneralSettingsService(IRepository<GeneralSetting> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GeneralSettingModel>> GetAllAsync()
        {
            try
            {
                List<GeneralSetting> generalSettings = await _repository.GetAllAsync();
                return _mapper.Map<List<GeneralSettingModel>>(generalSettings);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> UpdateGeneralSettingsAsync(List<GeneralSettingModel> generalSettings)
        {
            try
            {
                try
                {
                    _repository.BeginTransaction();
                    //Delete old records...
                    List<GeneralSetting> generalSettingsExisting = await _repository.GetAllAsync();
                    _repository.DeleteRange(generalSettingsExisting);
                    //Then insert again...
                    List<GeneralSetting> listOfGeneralSettings = new List<GeneralSetting>();

                    foreach (GeneralSettingModel generalSettingModel in generalSettings)
                    {
                        GeneralSetting generalSetting = new GeneralSetting();
                        generalSetting.SettingName = generalSettingModel.SettingName;
                        generalSetting.SettingValue = generalSettingModel.SettingValue;
                        listOfGeneralSettings.Add(generalSetting);
                    }
                    _repository.InsertRange(listOfGeneralSettings);
                    _repository.CommitTransaction();
                }
                catch (Exception)
                {
                    _repository.RollBackTransaction();
                    return false;
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

