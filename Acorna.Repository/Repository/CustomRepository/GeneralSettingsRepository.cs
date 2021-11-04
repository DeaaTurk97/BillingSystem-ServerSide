using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.Repository;
using Acorna.Core.Repository.ICustomRepsitory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acorna.Repository.Repository.CustomRepository
{
    public class GeneralSettingsRepository : Repository<GeneralSetting>, IGeneralSettingsRepository
    {
        private readonly IDbFactory _dbFactory;
        private readonly IUnitOfWork _unitOfWork;

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

        public string GetDefaultPassword()
        {
            try
            {
                return Convert.ToString(GeneralSettings.Find(x => x.SettingName == "DefaultPassword")?.SettingValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsDeleteFreeTypeNumber()
        {
            try
            {
                return Convert.ToBoolean(GeneralSettings.Find(x => x.SettingName == "IsDeleteFreeTypeNumber")?.SettingValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetSettingValueFromSettingName(string settingName)
        {
            try
            {
                return await _dbFactory.DataContext.GeneralSetting.Where(x => x.SettingName == settingName).Select(x => x.SettingValue).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsAutomatedApprovalBills()
        {
            try
            {
                return Convert.ToBoolean(GeneralSettings.Find(x => x.SettingName == "IsAutomatedApprovalBills")?.SettingValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsAutomatedApprovalOfNumbers()
        {
            try
            {
                return Convert.ToBoolean(GeneralSettings.Find(x => x.SettingName == "IsAutomatedApprovalPhoneNumbers")?.SettingValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsAutomatedApprovalServices()
        {
            try
            {
                return Convert.ToBoolean(GeneralSettings.Find(x => x.SettingName == "IsAutomatedApprovalServices")?.SettingValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
