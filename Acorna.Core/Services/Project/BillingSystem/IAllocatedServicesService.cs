using Acorna.Core.Models.Project.BillingSystem;
using System.Collections.Generic;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IAllocatedServicesService
    {
        List<AllocatedUsersServiceModel> GetAllAlocatedServices(int userId);
    }
}
