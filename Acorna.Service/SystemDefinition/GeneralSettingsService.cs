using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.IServices.Project;
using Acorna.Core.Models.SystemDefinition;
using Acorna.Core.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Service.SystemDefinition
{
    internal class GeneralSettingsService : IGeneralSettingsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        internal GeneralSettingsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GeneralSettingModel>> GetAllAsync()
        {
            try
            {
                List<GeneralSetting> generalSettings = await _unitOfWork.GetRepository<GeneralSetting>().GetAllAsync();
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
                    //Delete old records...
                    List<GeneralSetting> generalSettingsExisting = await _unitOfWork.GetRepository<GeneralSetting>().GetAllAsync();
                    _unitOfWork.GetRepository<GeneralSetting>().DeleteRange(generalSettingsExisting);
                    //Then insert again...
                    List<GeneralSetting> listOfGeneralSettings = new List<GeneralSetting>();

                    foreach (GeneralSettingModel generalSettingModel in generalSettings)
                    {
                        GeneralSetting generalSetting = new GeneralSetting();
                        generalSetting.SettingName = generalSettingModel.SettingName;
                        generalSetting.SettingValue = generalSettingModel.SettingValue;
                        listOfGeneralSettings.Add(generalSetting);
                    }
                    _unitOfWork.GetRepository<GeneralSetting>().InsertRange(listOfGeneralSettings);
                    _unitOfWork.SaveChanges();
                }
                catch (Exception)
                {
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

