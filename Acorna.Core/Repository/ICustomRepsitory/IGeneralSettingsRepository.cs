using Acorna.Core.Entity.SystemDefinition;

namespace Acorna.Core.Repository.ICustomRepsitory
{
    public interface IGeneralSettingsRepository : IRepository<GeneralSetting>
    {
        public bool IsDefaultPassword();
        public bool IsViewCallsThatHaveApriceEqualZero();
        public bool IsSendPasswordByEmail();
        public bool IsAbilityAdministratorToControlEmployeeBills();
        public bool IsReminderBySystem();
        public bool IsReminderByEmail();
    }
}
