using System.Collections.Generic;
using System.Threading.Tasks;
using Acorna.Core.Models.SystemDefinition;

namespace Acorna.Core.IServices.Project
{
    public interface IGeneralSettingsService
    {
        Task<List<GeneralSettingModel>> GetAllAsync();
        Task<bool> UpdateGeneralSettingsAsync(List<GeneralSettingModel> generalSettings);
    }
}
