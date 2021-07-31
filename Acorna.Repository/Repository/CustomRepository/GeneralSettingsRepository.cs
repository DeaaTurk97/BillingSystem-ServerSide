using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.Repository.ICustomRepsitory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Acorna.Repository.Repository.CustomRepository
{
    public class GeneralSettingsRepository : Repository<GeneralSetting>, IGeneralSettingsRepository
    {
        private readonly IDbFactory _dbFactory;

        public GeneralSettingsRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<GeneralSetting> GeneralSettings
        {
            get
            {
                return _dbFactory.DataContext.GeneralSetting.ToList();
            }
        }

        public bool IsDefaultPassword()
        {
            try
            {
                return Convert.ToBoolean(GeneralSettings.Find(x => x.SettingName == "IsDefaultPassword")?.SettingValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsSendPasswordByEmail()
        {
            try
            {
                return Convert.ToBoolean(GeneralSettings.Find(x => x.SettingName == "IsSendPasswordByEmail")?.SettingValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsViewCallsThatHaveApriceEqualZero()
        {
            try
            {
                return Convert.ToBoolean(GeneralSettings.Find(x => x.SettingName == "IsViewCallsThatHaveApriceEqualZero")?.SettingValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsAbilityAdministratorToControlEmployeeBills()
        {
            try
            {
                return Convert.ToBoolean(GeneralSettings.Find(x => x.SettingName == "IsAbilityOfTheAdministratorToControlEmployeeBills")?.SettingValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsReminderBySystem()
        {
            try
            {
                return Convert.ToBoolean(GeneralSettings.Find(x => x.SettingName == "IsReminderBySystem")?.SettingValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsReminderByEmail()
        {
            try
            {
                return Convert.ToBoolean(GeneralSettings.Find(x => x.SettingName == "IsReminderByEmail")?.SettingValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
