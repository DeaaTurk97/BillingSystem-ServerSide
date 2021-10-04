using Acorna.Core.Models.Project.BillingSystem;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface ISimProfileService
    {
        Task<List<SimProfileModel>> GetAllSimProfile();
    }
}
