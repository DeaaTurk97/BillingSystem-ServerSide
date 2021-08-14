using Acorna.Core.Entity.SystemDefinition;

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

    }
}
