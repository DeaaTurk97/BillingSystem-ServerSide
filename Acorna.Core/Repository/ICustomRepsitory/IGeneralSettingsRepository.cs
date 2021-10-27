using Acorna.Core.Entity.SystemDefinition;
using System.Threading.Tasks;

namespace Acorna.Core.Repository.ICustomRepsitory
{
    public interface IGeneralSettingsRepository : IRepository<GeneralSetting>
    {
        bool IsDefaultPassword();
        bool IsViewCallsThatHaveApriceEqualZero();
        bool IsSendPasswordByEmail();
        bool IsAbilityAdministratorToControlEmployeeBills();
        bool IsReminderBySystem();
        bool IsReminderByEmail();
        string GetDefaultPassword();
        bool IsDeleteFreeTypeNumber();
        Task<string> GetSettingValueFromSettingName(string settingName);
        bool IsAutomatedApprovalOfNumbers();
        bool IsAutomatedApprovalServices();
    }
}
